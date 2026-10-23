// Add a reference to a producer element that uses a type from a THIRD module
// (e.g. an action whose output is a structure defined in another module).
// Run in the CONSUMER's edit session via applyModelApiCode.
// imports: ["OutSystems.Model", "OutSystems.Model.Signatures", "OutSystems.Model.Enumerations"]
//
// Both the producer and the third module must be loaded from OML (open each in
// Service Studio, copy its -in.oml). Remove the final throw to apply, then omlMerge.
// The reference to the third module (with just the requested type) is added automatically.

eSpace => {
    var dir = @"C:\path\to\omls\";
    var producer = Services.ModelServices.LoadESpace(dir + "Producer.oml");
    var third = Services.ModelServices.LoadESpace(dir + "ThirdModule.oml");
    if (producer.Name != "Producer" || third.Name != "ThirdModule")
        throw new Exception("Wrong OML: " + producer.Name + " / " + third.Name);

    var si = producer.GetSignatureInformation();
    si.TargetElementKeys = new[] { producer.ServerActions.Named("SomeAction").ObjectKey };

    var log = new List<string>();
    // Must be a declared Func: an inline lambda is ambiguous with the Task<> overload (CS0121).
    Func<IKey, SignatureInformation> fetch = k => {
        if (k.ToString() == third.ObjectKey.ToString()) {
            log.Add(k + "=>third");
            var tsi = third.GetSignatureInformation();
            tsi.TargetElementKeys = new[] { third.Structures.Named("SomeStructure").ObjectKey };
            return tsi;
        }
        // Modules the consumer already references (e.g. (System)): metadata is enough.
        // Never return null (NullReferenceException).
        var pr = producer.References.FirstOrDefault(r => r.ModuleKey != null && r.ModuleKey.ToString() == k.ToString());
        log.Add(k + "=>meta:" + (pr == null ? "?" : pr.Name));
        return new BinarySignatureInformation {
            Key = k, Name = pr == null ? null : pr.Name,
            ModuleKind = ReferenceKind.eSpace, TargetElementKeys = new IKey[0]
        };
    };

    var added = eSpace.AddDependency(si, fetch).ToList();
    // added=[] with no error = a needed signature was missing. Fail loudly.
    if (!added.Any(o => o.ToString().StartsWith("SomeAction")))
        throw new Exception("SomeAction not added; fetch log=[" + string.Join(",", log) + "]");

    throw new Exception("PROBE:added=[" + string.Join(",", added.Select(o => o.GetType().Name + ":" + o.ToString()))
        + "] fetch=[" + string.Join(",", log) + "] refs=[" + string.Join(",", eSpace.References.Select(r => r.Name)) + "]");
}
