﻿//------------------------------------------------------------------------------
/// <autogenerated>
///     This code was generated by a tool.
///     Runtime Version: 4.0.30319.42000
///
///     Changes to this file may cause incorrect behavior and will be lost if 
///     the code is regenerated.
/// </autogenerated>
//------------------------------------------------------------------------------

//@cc_on
//@set @debug(off)

import System.Xml.Serialization;

//
//This source code was auto-generated by xsd, Version=4.8.3928.0.
//

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.fivesqd.com/schemas/veml/1.0") System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0", IsNullable=false) 
class veml {
    
    private var metadataField : vemlMetadata;
    
    private var environmentField : vemlEnvironment;
    
    ///<remarks/>
    public final function get metadata() : vemlMetadata {
        return this.metadataField;
    }
    public final function set metadata(value : vemlMetadata) {
        this.metadataField = value;
    }
    
    ///<remarks/>
    public final function get environment() : vemlEnvironment {
        return this.environmentField;
    }
    public final function set environment(value : vemlEnvironment) {
        this.environmentField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class vemlMetadata {
    
    private var titleField : System.String;
    
    private var scriptField : System.String[];
    
    private var inputeventField : inputevent[];
    
    private var synchronizationserviceField : synchronizationservice[];
    
    ///<remarks/>
    public final function get title() : System.String {
        return this.titleField;
    }
    public final function set title(value : System.String) {
        this.titleField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlElementAttribute("script") 
    function get script() : System.String[] {
        return this.scriptField;
    }
    public final function set script(value : System.String[]) {
        this.scriptField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlElementAttribute("inputevent") 
    function get inputevent() : inputevent[] {
        return this.inputeventField;
    }
    public final function set inputevent(value : inputevent[]) {
        this.inputeventField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlElementAttribute("synchronizationservice") 
    function get synchronizationservice() : synchronizationservice[] {
        return this.synchronizationserviceField;
    }
    public final function set synchronizationservice(value : synchronizationservice[]) {
        this.synchronizationserviceField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class inputevent {
    
    private var inputField : System.String;
    
    private var eventField : System.String;
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get input() : System.String {
        return this.inputField;
    }
    public final function set input(value : System.String) {
        this.inputField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get \event() : System.String {
        return this.eventField;
    }
    public final function set \event(value : System.String) {
        this.eventField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(TypeName="size-percent", Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class sizepercent {
    
    private var xField : double;
    
    private var yField : double;
    
    public function sizepercent() {
        this.xField = 1;
        this.yField = 1;
        
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(1) 
    function get x() : double {
        return this.xField;
    }
    public final function set x(value : double) {
        this.xField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(1) 
    function get y() : double {
        return this.yField;
    }
    public final function set y(value : double) {
        this.yField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(TypeName="position-percent", Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class positionpercent {
    
    private var xField : double;
    
    private var yField : double;
    
    public function positionpercent() {
        this.xField = 0;
        this.yField = 0;
        
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(0) 
    function get x() : double {
        return this.xField;
    }
    public final function set x(value : double) {
        this.xField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(0) 
    function get y() : double {
        return this.yField;
    }
    public final function set y(value : double) {
        this.yField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class size {
    
    private var xField : double;
    
    private var yField : double;
    
    private var zField : double;
    
    public function size() {
        this.xField = 1;
        this.yField = 1;
        this.zField = 1;
        
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(1) 
    function get x() : double {
        return this.xField;
    }
    public final function set x(value : double) {
        this.xField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(1) 
    function get y() : double {
        return this.yField;
    }
    public final function set y(value : double) {
        this.yField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(1) 
    function get z() : double {
        return this.zField;
    }
    public final function set z(value : double) {
        this.zField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class scale {
    
    private var xField : double;
    
    private var yField : double;
    
    private var zField : double;
    
    public function scale() {
        this.xField = 1;
        this.yField = 1;
        this.zField = 1;
        
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(1) 
    function get x() : double {
        return this.xField;
    }
    public final function set x(value : double) {
        this.xField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(1) 
    function get y() : double {
        return this.yField;
    }
    public final function set y(value : double) {
        this.yField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(1) 
    function get z() : double {
        return this.zField;
    }
    public final function set z(value : double) {
        this.zField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class rotation {
    
    private var xField : double;
    
    private var yField : double;
    
    private var zField : double;
    
    private var wField : double;
    
    public function rotation() {
        this.xField = 0;
        this.yField = 0;
        this.zField = 0;
        this.wField = 1;
        
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(0) 
    function get x() : double {
        return this.xField;
    }
    public final function set x(value : double) {
        this.xField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(0) 
    function get y() : double {
        return this.yField;
    }
    public final function set y(value : double) {
        this.yField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(0) 
    function get z() : double {
        return this.zField;
    }
    public final function set z(value : double) {
        this.zField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(1) 
    function get w() : double {
        return this.wField;
    }
    public final function set w(value : double) {
        this.wField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class position {
    
    private var xField : double;
    
    private var yField : double;
    
    private var zField : double;
    
    public function position() {
        this.xField = 0;
        this.yField = 0;
        this.zField = 0;
        
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(0) 
    function get x() : double {
        return this.xField;
    }
    public final function set x(value : double) {
        this.xField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(0) 
    function get y() : double {
        return this.yField;
    }
    public final function set y(value : double) {
        this.yField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() System.ComponentModel.DefaultValueAttribute(0) 
    function get z() : double {
        return this.zField;
    }
    public final function set z(value : double) {
        this.zField = value;
    }
}

///<remarks/>
public System.Xml.Serialization.XmlIncludeAttribute(canvastransform) System.Xml.Serialization.XmlIncludeAttribute(sizetransform) System.Xml.Serialization.XmlIncludeAttribute(scaletransform) System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class basetransform {
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class canvastransform extends basetransform {
    
    private var positionpercentField : positionpercent;
    
    private var sizepercentField : sizepercent;
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlElementAttribute("position-percent") 
    function get positionpercent() : positionpercent {
        return this.positionpercentField;
    }
    public final function set positionpercent(value : positionpercent) {
        this.positionpercentField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlElementAttribute("size-percent") 
    function get sizepercent() : sizepercent {
        return this.sizepercentField;
    }
    public final function set sizepercent(value : sizepercent) {
        this.sizepercentField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class sizetransform extends basetransform {
    
    private var positionField : position;
    
    private var rotationField : rotation;
    
    private var sizeField : size;
    
    ///<remarks/>
    public final function get position() : position {
        return this.positionField;
    }
    public final function set position(value : position) {
        this.positionField = value;
    }
    
    ///<remarks/>
    public final function get rotation() : rotation {
        return this.rotationField;
    }
    public final function set rotation(value : rotation) {
        this.rotationField = value;
    }
    
    ///<remarks/>
    public final function get size() : size {
        return this.sizeField;
    }
    public final function set size(value : size) {
        this.sizeField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class scaletransform extends basetransform {
    
    private var positionField : position;
    
    private var rotationField : rotation;
    
    private var scaleField : scale;
    
    ///<remarks/>
    public final function get position() : position {
        return this.positionField;
    }
    public final function set position(value : position) {
        this.positionField = value;
    }
    
    ///<remarks/>
    public final function get rotation() : rotation {
        return this.rotationField;
    }
    public final function set rotation(value : rotation) {
        this.rotationField = value;
    }
    
    ///<remarks/>
    public final function get scale() : scale {
        return this.scaleField;
    }
    public final function set scale(value : scale) {
        this.scaleField = value;
    }
}

///<remarks/>
public System.Xml.Serialization.XmlIncludeAttribute(voxelentity) System.Xml.Serialization.XmlIncludeAttribute(textentity) System.Xml.Serialization.XmlIncludeAttribute(terrainentity) System.Xml.Serialization.XmlIncludeAttribute(lightentity) System.Xml.Serialization.XmlIncludeAttribute(inputentity) System.Xml.Serialization.XmlIncludeAttribute(buttonentity) System.Xml.Serialization.XmlIncludeAttribute(canvasentity) System.Xml.Serialization.XmlIncludeAttribute(characterentity) System.Xml.Serialization.XmlIncludeAttribute(meshentity) System.Xml.Serialization.XmlIncludeAttribute(containerentity) System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class entity {
    
    private var transformField : basetransform;
    
    private var entity1Field : entity[];
    
    private var synchronizerField : System.String;
    
    private var tagField : System.String;
    
    private var idField : System.String;
    
    private var onloadeventField : System.String;
    
    ///<remarks/>
    public final function get transform() : basetransform {
        return this.transformField;
    }
    public final function set transform(value : basetransform) {
        this.transformField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlElementAttribute("entity") 
    function get entity1() : entity[] {
        return this.entity1Field;
    }
    public final function set entity1(value : entity[]) {
        this.entity1Field = value;
    }
    
    ///<remarks/>
    public final function get synchronizer() : System.String {
        return this.synchronizerField;
    }
    public final function set synchronizer(value : System.String) {
        this.synchronizerField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get tag() : System.String {
        return this.tagField;
    }
    public final function set tag(value : System.String) {
        this.tagField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get id() : System.String {
        return this.idField;
    }
    public final function set id(value : System.String) {
        this.idField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute("on-load-event") 
    function get onloadevent() : System.String {
        return this.onloadeventField;
    }
    public final function set onloadevent(value : System.String) {
        this.onloadeventField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class voxelentity extends entity {
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class textentity extends entity {
    
    private var textField : System.String;
    
    private var fontsizeField : float;
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get text() : System.String {
        return this.textField;
    }
    public final function set text(value : System.String) {
        this.textField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute("font-size") 
    function get fontsize() : float {
        return this.fontsizeField;
    }
    public final function set fontsize(value : float) {
        this.fontsizeField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class terrainentity extends entity {
    
    private var lengthField : double;
    
    private var widthField : double;
    
    private var heightField : double;
    
    private var heightsField : System.String;
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get length() : double {
        return this.lengthField;
    }
    public final function set length(value : double) {
        this.lengthField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get width() : double {
        return this.widthField;
    }
    public final function set width(value : double) {
        this.widthField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get height() : double {
        return this.heightField;
    }
    public final function set height(value : double) {
        this.heightField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get heights() : System.String {
        return this.heightsField;
    }
    public final function set heights(value : System.String) {
        this.heightsField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class lightentity extends entity {
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class inputentity extends entity {
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class buttonentity extends entity {
    
    private var onclickeventField : System.String;
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute("on-click-event") 
    function get onclickevent() : System.String {
        return this.onclickeventField;
    }
    public final function set onclickevent(value : System.String) {
        this.onclickeventField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class canvasentity extends entity {
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class characterentity extends entity {
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class meshentity extends entity {
    
    private var meshnameField : System.String;
    
    private var meshresourceField : System.String[];
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlElementAttribute("mesh-name") 
    function get meshname() : System.String {
        return this.meshnameField;
    }
    public final function set meshname(value : System.String) {
        this.meshnameField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlElementAttribute("mesh-resource") 
    function get meshresource() : System.String[] {
        return this.meshresourceField;
    }
    public final function set meshresource(value : System.String[]) {
        this.meshresourceField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class containerentity extends entity {
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class background {
    
    private var itemField : System.String;
    
    private var itemElementNameField : ItemChoiceType;
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlElementAttribute("color", System.String) System.Xml.Serialization.XmlElementAttribute("panorama", System.String) System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName") 
    function get Item() : System.String {
        return this.itemField;
    }
    public final function set Item(value : System.String) {
        this.itemField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlIgnoreAttribute() 
    function get ItemElementName() : ItemChoiceType {
        return this.itemElementNameField;
    }
    public final function set ItemElementName(value : ItemChoiceType) {
        this.itemElementNameField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0", IncludeInSchema=false) 
enum ItemChoiceType {
    
    ///<remarks/>
    color,
    
    ///<remarks/>
    panorama,
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class synchronizationservice {
    
    private var typeField : System.String;
    
    private var addressField : System.String;
    
    private var idField : System.String;
    
    private var sessionField : System.String;
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get type() : System.String {
        return this.typeField;
    }
    public final function set type(value : System.String) {
        this.typeField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get address() : System.String {
        return this.addressField;
    }
    public final function set address(value : System.String) {
        this.addressField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get id() : System.String {
        return this.idField;
    }
    public final function set id(value : System.String) {
        this.idField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlAttributeAttribute() 
    function get session() : System.String {
        return this.sessionField;
    }
    public final function set session(value : System.String) {
        this.sessionField = value;
    }
}

///<remarks/>
public System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0") System.SerializableAttribute() System.Diagnostics.DebuggerStepThroughAttribute() System.ComponentModel.DesignerCategoryAttribute("code") System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.fivesqd.com/schemas/veml/1.0") 
class vemlEnvironment {
    
    private var backgroundField : background;
    
    private var entityField : entity[];
    
    ///<remarks/>
    public final function get background() : background {
        return this.backgroundField;
    }
    public final function set background(value : background) {
        this.backgroundField = value;
    }
    
    ///<remarks/>
    public final System.Xml.Serialization.XmlElementAttribute("entity") 
    function get entity() : entity[] {
        return this.entityField;
    }
    public final function set entity(value : entity[]) {
        this.entityField = value;
    }
}