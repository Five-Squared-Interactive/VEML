﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0", IsNullable=false)]
public partial class veml {
    
    private vemlMetadata metadataField;
    
    private vemlEnvironment environmentField;
    
    /// <remarks/>
    public vemlMetadata metadata {
        get {
            return this.metadataField;
        }
        set {
            this.metadataField = value;
        }
    }
    
    /// <remarks/>
    public vemlEnvironment environment {
        get {
            return this.environmentField;
        }
        set {
            this.environmentField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class vemlMetadata {
    
    private string titleField;
    
    private string[] scriptField;
    
    private inputevent[] inputeventField;
    
    private synchronizationservice[] synchronizationserviceField;
    
    /// <remarks/>
    public string title {
        get {
            return this.titleField;
        }
        set {
            this.titleField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("script")]
    public string[] script {
        get {
            return this.scriptField;
        }
        set {
            this.scriptField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("inputevent")]
    public inputevent[] inputevent {
        get {
            return this.inputeventField;
        }
        set {
            this.inputeventField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("synchronizationservice")]
    public synchronizationservice[] synchronizationservice {
        get {
            return this.synchronizationserviceField;
        }
        set {
            this.synchronizationserviceField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class inputevent {
    
    private string inputField;
    
    private string eventField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string input {
        get {
            return this.inputField;
        }
        set {
            this.inputField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string @event {
        get {
            return this.eventField;
        }
        set {
            this.eventField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName="size-percent", Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class sizepercent {
    
    private double xField;
    
    private double yField;
    
    public sizepercent() {
        this.xField = 1D;
        this.yField = 1D;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(1D)]
    public double x {
        get {
            return this.xField;
        }
        set {
            this.xField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(1D)]
    public double y {
        get {
            return this.yField;
        }
        set {
            this.yField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName="position-percent", Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class positionpercent {
    
    private double xField;
    
    private double yField;
    
    public positionpercent() {
        this.xField = 0D;
        this.yField = 0D;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(0D)]
    public double x {
        get {
            return this.xField;
        }
        set {
            this.xField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(0D)]
    public double y {
        get {
            return this.yField;
        }
        set {
            this.yField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class size {
    
    private double xField;
    
    private double yField;
    
    private double zField;
    
    public size() {
        this.xField = 1D;
        this.yField = 1D;
        this.zField = 1D;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(1D)]
    public double x {
        get {
            return this.xField;
        }
        set {
            this.xField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(1D)]
    public double y {
        get {
            return this.yField;
        }
        set {
            this.yField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(1D)]
    public double z {
        get {
            return this.zField;
        }
        set {
            this.zField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class scale {
    
    private double xField;
    
    private double yField;
    
    private double zField;
    
    public scale() {
        this.xField = 1D;
        this.yField = 1D;
        this.zField = 1D;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(1D)]
    public double x {
        get {
            return this.xField;
        }
        set {
            this.xField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(1D)]
    public double y {
        get {
            return this.yField;
        }
        set {
            this.yField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(1D)]
    public double z {
        get {
            return this.zField;
        }
        set {
            this.zField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class rotation {
    
    private double xField;
    
    private double yField;
    
    private double zField;
    
    private double wField;
    
    public rotation() {
        this.xField = 0D;
        this.yField = 0D;
        this.zField = 0D;
        this.wField = 1D;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(0D)]
    public double x {
        get {
            return this.xField;
        }
        set {
            this.xField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(0D)]
    public double y {
        get {
            return this.yField;
        }
        set {
            this.yField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(0D)]
    public double z {
        get {
            return this.zField;
        }
        set {
            this.zField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(1D)]
    public double w {
        get {
            return this.wField;
        }
        set {
            this.wField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class position {
    
    private double xField;
    
    private double yField;
    
    private double zField;
    
    public position() {
        this.xField = 0D;
        this.yField = 0D;
        this.zField = 0D;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(0D)]
    public double x {
        get {
            return this.xField;
        }
        set {
            this.xField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(0D)]
    public double y {
        get {
            return this.yField;
        }
        set {
            this.yField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute(0D)]
    public double z {
        get {
            return this.zField;
        }
        set {
            this.zField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(canvastransform))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(sizetransform))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(scaletransform))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class basetransform {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class canvastransform : basetransform {
    
    private positionpercent positionpercentField;
    
    private sizepercent sizepercentField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("position-percent")]
    public positionpercent positionpercent {
        get {
            return this.positionpercentField;
        }
        set {
            this.positionpercentField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("size-percent")]
    public sizepercent sizepercent {
        get {
            return this.sizepercentField;
        }
        set {
            this.sizepercentField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class sizetransform : basetransform {
    
    private position positionField;
    
    private rotation rotationField;
    
    private size sizeField;
    
    /// <remarks/>
    public position position {
        get {
            return this.positionField;
        }
        set {
            this.positionField = value;
        }
    }
    
    /// <remarks/>
    public rotation rotation {
        get {
            return this.rotationField;
        }
        set {
            this.rotationField = value;
        }
    }
    
    /// <remarks/>
    public size size {
        get {
            return this.sizeField;
        }
        set {
            this.sizeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class scaletransform : basetransform {
    
    private position positionField;
    
    private rotation rotationField;
    
    private scale scaleField;
    
    /// <remarks/>
    public position position {
        get {
            return this.positionField;
        }
        set {
            this.positionField = value;
        }
    }
    
    /// <remarks/>
    public rotation rotation {
        get {
            return this.rotationField;
        }
        set {
            this.rotationField = value;
        }
    }
    
    /// <remarks/>
    public scale scale {
        get {
            return this.scaleField;
        }
        set {
            this.scaleField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(voxelentity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(textentity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(terrainentity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(lightentity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(inputentity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(buttonentity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(canvasentity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(characterentity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(meshentity))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(containerentity))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class entity {
    
    private basetransform transformField;
    
    private entity[] entity1Field;
    
    private string synchronizerField;
    
    private string tagField;
    
    private string idField;
    
    private string onloadeventField;
    
    /// <remarks/>
    public basetransform transform {
        get {
            return this.transformField;
        }
        set {
            this.transformField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("entity")]
    public entity[] entity1 {
        get {
            return this.entity1Field;
        }
        set {
            this.entity1Field = value;
        }
    }
    
    /// <remarks/>
    public string synchronizer {
        get {
            return this.synchronizerField;
        }
        set {
            this.synchronizerField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string tag {
        get {
            return this.tagField;
        }
        set {
            this.tagField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string id {
        get {
            return this.idField;
        }
        set {
            this.idField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute("on-load-event")]
    public string onloadevent {
        get {
            return this.onloadeventField;
        }
        set {
            this.onloadeventField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class voxelentity : entity {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class textentity : entity {
    
    private string textField;
    
    private float fontsizeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string text {
        get {
            return this.textField;
        }
        set {
            this.textField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute("font-size")]
    public float fontsize {
        get {
            return this.fontsizeField;
        }
        set {
            this.fontsizeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class terrainentity : entity {
    
    private double lengthField;
    
    private double widthField;
    
    private double heightField;
    
    private string heightsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public double length {
        get {
            return this.lengthField;
        }
        set {
            this.lengthField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public double width {
        get {
            return this.widthField;
        }
        set {
            this.widthField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public double height {
        get {
            return this.heightField;
        }
        set {
            this.heightField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string heights {
        get {
            return this.heightsField;
        }
        set {
            this.heightsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class lightentity : entity {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class inputentity : entity {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class buttonentity : entity {
    
    private string onclickeventField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute("on-click-event")]
    public string onclickevent {
        get {
            return this.onclickeventField;
        }
        set {
            this.onclickeventField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class canvasentity : entity {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class characterentity : entity {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class meshentity : entity {
    
    private string meshnameField;
    
    private string[] meshresourceField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("mesh-name")]
    public string meshname {
        get {
            return this.meshnameField;
        }
        set {
            this.meshnameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("mesh-resource")]
    public string[] meshresource {
        get {
            return this.meshresourceField;
        }
        set {
            this.meshresourceField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class containerentity : entity {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class background {
    
    private string itemField;
    
    private ItemChoiceType itemElementNameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("color", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("panorama", typeof(string))]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
    public string Item {
        get {
            return this.itemField;
        }
        set {
            this.itemField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemChoiceType ItemElementName {
        get {
            return this.itemElementNameField;
        }
        set {
            this.itemElementNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0", IncludeInSchema=false)]
public enum ItemChoiceType {
    
    /// <remarks/>
    color,
    
    /// <remarks/>
    panorama,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class synchronizationservice {
    
    private string typeField;
    
    private string addressField;
    
    private string idField;
    
    private string sessionField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type {
        get {
            return this.typeField;
        }
        set {
            this.typeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string address {
        get {
            return this.addressField;
        }
        set {
            this.addressField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string id {
        get {
            return this.idField;
        }
        set {
            this.idField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string session {
        get {
            return this.sessionField;
        }
        set {
            this.sessionField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.fivesqd.com/schemas/veml/1.0")]
public partial class vemlEnvironment {
    
    private background backgroundField;
    
    private entity[] entityField;
    
    /// <remarks/>
    public background background {
        get {
            return this.backgroundField;
        }
        set {
            this.backgroundField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("entity")]
    public entity[] entity {
        get {
            return this.entityField;
        }
        set {
            this.entityField = value;
        }
    }
}
