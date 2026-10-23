// Add a reference to selected public elements of another module (producer).
// Run in the CONSUMER's edit session via applyModelApiCode.
// imports: ["OutSystems.Model", "OutSystems.Model.Signatures"]
//
// Keep the final throw for the probe (nothing is saved); remove it to apply,
// then omlMerge the returned mutatedOmlPath.

eSpace => {
    var producerPath = @"C:\path\to\Producer.oml";   // copy of the producer's -in.oml
    var producerName = "Producer";

    var producer = Services.ModelServices.LoadESpace(producerPath);
    if (producer.Name != producerName)
        throw new Exception("Wrong OML: loaded '" + producer.Name + "', expected '" + producerName + "'");

    var si = producer.GetSignatureInformation();
    si.TargetElementKeys = new[] {
        producer.ServerActions.Named("SomeAction").ObjectKey,
        producer.Entities.Named("SomeEntity").ObjectKey,
        // producer.Structures.Named("SomeStructure").ObjectKey,
    };

    var added = eSpace.AddDependency(si).ToList();
    // added=[] with no error = an element uses a type from a third module.
    // Use examples/AddReferenceWithThirdModuleType.cs instead.
    if (added.Count == 0)
        throw new Exception("Nothing added: a requested element probably needs a third module's signature");

    var r = eSpace.References.Named(producerName);
    throw new Exception("PROBE:added=" + string.Join(",", added.Select(o => o.GetType().Name + ":" + o.ToString()))
        + " refEntities=[" + string.Join(",", r.Entities.Select(e => e.Name))
        + "] refActions=[" + string.Join(",", r.ServerActions.Select(a => a.Name)) + "]");
}
