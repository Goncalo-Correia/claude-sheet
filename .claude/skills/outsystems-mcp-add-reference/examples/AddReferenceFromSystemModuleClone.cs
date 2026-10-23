// Add a reference to an element of a system module that Service Studio only opens as an
// unsaved clone (e.g. Users -> CloneOfUsers). The clone's signature bytes are passed with the
// ORIGINAL module's key and name, so the element joins the real Users reference.
// Run in the CONSUMER's edit session via applyModelApiCode.
// imports: ["OutSystems.Model", "OutSystems.Model.Signatures", "OutSystems.Model.Enumerations"]
//
// Keep the final throw for the probe; remove it to apply, then omlMerge.

eSpace => {
    var clone = Services.ModelServices.LoadESpace(@"C:\path\to\CloneOfUsers.oml");
    var originalName = "Users";
    // The consumer already references Users here. Otherwise use
    // Services.ModelServices.ParseKey("<original compact module key>").
    var originalKey = eSpace.References.Named(originalName).ModuleKey;

    var csi = clone.GetSignatureInformation();
    var si = new BinarySignatureInformation {
        Key = originalKey, Name = originalName, Description = csi.Description,
        ModuleKind = ReferenceKind.eSpace, ModuleType = csi.ModuleType,
        SignatureBinary = clone.GetSignatureBinary(),
        // Element keys are the same in the clone and in the original.
        TargetElementKeys = new[] { clone.ServerActions.Named("User_GetIdByUsername").ObjectKey }
    };

    var log = new List<string>();
    // Called for (System) and for the clone's own key: metadata-only is enough. Never return null.
    Func<IKey, SignatureInformation> fetch = k => {
        log.Add(k.ToString());
        return new BinarySignatureInformation { Key = k, ModuleKind = ReferenceKind.eSpace, TargetElementKeys = new IKey[0] };
    };

    var added = eSpace.AddDependency(si, fetch).ToList();
    if (!added.Any(o => o.ToString().StartsWith("User_GetIdByUsername")))
        throw new Exception("Not added; fetch=[" + string.Join(",", log) + "]");
    if (eSpace.References.Any(r => r.Name == clone.Name))
        throw new Exception("A reference to the clone was created");

    var r = eSpace.References.Named(originalName);
    throw new Exception("PROBE:added=[" + string.Join(",", added.Select(o => o.ToString()))
        + "] " + originalName + "=" + r.ModuleKey + " actions=[" + string.Join(",", r.ServerActions.Select(a => a.Name)) + "]");
}
