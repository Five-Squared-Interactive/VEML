﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System.Xml.Serialization

'
'This source code was auto-generated by xsd, Version=4.8.3928.0.
'

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.fivesqd.com/schemas/veml/1.0"),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0", IsNullable:=false)>  _
Partial Public Class veml
    
    Private metadataField As vemlMetadata
    
    Private environmentField As vemlEnvironment
    
    '''<remarks/>
    Public Property metadata() As vemlMetadata
        Get
            Return Me.metadataField
        End Get
        Set
            Me.metadataField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property environment() As vemlEnvironment
        Get
            Return Me.environmentField
        End Get
        Set
            Me.environmentField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class vemlMetadata
    
    Private titleField As String
    
    Private scriptField() As String
    
    Private inputeventField() As inputevent
    
    Private synchronizationserviceField() As synchronizationservice
    
    '''<remarks/>
    Public Property title() As String
        Get
            Return Me.titleField
        End Get
        Set
            Me.titleField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("script")>  _
    Public Property script() As String()
        Get
            Return Me.scriptField
        End Get
        Set
            Me.scriptField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("inputevent")>  _
    Public Property inputevent() As inputevent()
        Get
            Return Me.inputeventField
        End Get
        Set
            Me.inputeventField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("synchronizationservice")>  _
    Public Property synchronizationservice() As synchronizationservice()
        Get
            Return Me.synchronizationserviceField
        End Get
        Set
            Me.synchronizationserviceField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class inputevent
    
    Private inputField As String
    
    Private eventField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property input() As String
        Get
            Return Me.inputField
        End Get
        Set
            Me.inputField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property [event]() As String
        Get
            Return Me.eventField
        End Get
        Set
            Me.eventField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="size-percent", [Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class sizepercent
    
    Private xField As Double
    
    Private yField As Double
    
    Public Sub New()
        MyBase.New
        Me.xField = 1R
        Me.yField = 1R
    End Sub
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(1R)>  _
    Public Property x() As Double
        Get
            Return Me.xField
        End Get
        Set
            Me.xField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(1R)>  _
    Public Property y() As Double
        Get
            Return Me.yField
        End Get
        Set
            Me.yField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(TypeName:="position-percent", [Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class positionpercent
    
    Private xField As Double
    
    Private yField As Double
    
    Public Sub New()
        MyBase.New
        Me.xField = 0R
        Me.yField = 0R
    End Sub
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(0R)>  _
    Public Property x() As Double
        Get
            Return Me.xField
        End Get
        Set
            Me.xField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(0R)>  _
    Public Property y() As Double
        Get
            Return Me.yField
        End Get
        Set
            Me.yField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class size
    
    Private xField As Double
    
    Private yField As Double
    
    Private zField As Double
    
    Public Sub New()
        MyBase.New
        Me.xField = 1R
        Me.yField = 1R
        Me.zField = 1R
    End Sub
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(1R)>  _
    Public Property x() As Double
        Get
            Return Me.xField
        End Get
        Set
            Me.xField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(1R)>  _
    Public Property y() As Double
        Get
            Return Me.yField
        End Get
        Set
            Me.yField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(1R)>  _
    Public Property z() As Double
        Get
            Return Me.zField
        End Get
        Set
            Me.zField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class scale
    
    Private xField As Double
    
    Private yField As Double
    
    Private zField As Double
    
    Public Sub New()
        MyBase.New
        Me.xField = 1R
        Me.yField = 1R
        Me.zField = 1R
    End Sub
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(1R)>  _
    Public Property x() As Double
        Get
            Return Me.xField
        End Get
        Set
            Me.xField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(1R)>  _
    Public Property y() As Double
        Get
            Return Me.yField
        End Get
        Set
            Me.yField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(1R)>  _
    Public Property z() As Double
        Get
            Return Me.zField
        End Get
        Set
            Me.zField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class rotation
    
    Private xField As Double
    
    Private yField As Double
    
    Private zField As Double
    
    Private wField As Double
    
    Public Sub New()
        MyBase.New
        Me.xField = 0R
        Me.yField = 0R
        Me.zField = 0R
        Me.wField = 1R
    End Sub
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(0R)>  _
    Public Property x() As Double
        Get
            Return Me.xField
        End Get
        Set
            Me.xField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(0R)>  _
    Public Property y() As Double
        Get
            Return Me.yField
        End Get
        Set
            Me.yField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(0R)>  _
    Public Property z() As Double
        Get
            Return Me.zField
        End Get
        Set
            Me.zField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(1R)>  _
    Public Property w() As Double
        Get
            Return Me.wField
        End Get
        Set
            Me.wField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class position
    
    Private xField As Double
    
    Private yField As Double
    
    Private zField As Double
    
    Public Sub New()
        MyBase.New
        Me.xField = 0R
        Me.yField = 0R
        Me.zField = 0R
    End Sub
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(0R)>  _
    Public Property x() As Double
        Get
            Return Me.xField
        End Get
        Set
            Me.xField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(0R)>  _
    Public Property y() As Double
        Get
            Return Me.yField
        End Get
        Set
            Me.yField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(),  _
     System.ComponentModel.DefaultValueAttribute(0R)>  _
    Public Property z() As Double
        Get
            Return Me.zField
        End Get
        Set
            Me.zField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlIncludeAttribute(GetType(canvastransform)),  _
 System.Xml.Serialization.XmlIncludeAttribute(GetType(sizetransform)),  _
 System.Xml.Serialization.XmlIncludeAttribute(GetType(scaletransform)),  _
 System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class basetransform
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class canvastransform
    Inherits basetransform
    
    Private positionpercentField As positionpercent
    
    Private sizepercentField As sizepercent
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("position-percent")>  _
    Public Property positionpercent() As positionpercent
        Get
            Return Me.positionpercentField
        End Get
        Set
            Me.positionpercentField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("size-percent")>  _
    Public Property sizepercent() As sizepercent
        Get
            Return Me.sizepercentField
        End Get
        Set
            Me.sizepercentField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class sizetransform
    Inherits basetransform
    
    Private positionField As position
    
    Private rotationField As rotation
    
    Private sizeField As size
    
    '''<remarks/>
    Public Property position() As position
        Get
            Return Me.positionField
        End Get
        Set
            Me.positionField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property rotation() As rotation
        Get
            Return Me.rotationField
        End Get
        Set
            Me.rotationField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property size() As size
        Get
            Return Me.sizeField
        End Get
        Set
            Me.sizeField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class scaletransform
    Inherits basetransform
    
    Private positionField As position
    
    Private rotationField As rotation
    
    Private scaleField As scale
    
    '''<remarks/>
    Public Property position() As position
        Get
            Return Me.positionField
        End Get
        Set
            Me.positionField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property rotation() As rotation
        Get
            Return Me.rotationField
        End Get
        Set
            Me.rotationField = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property scale() As scale
        Get
            Return Me.scaleField
        End Get
        Set
            Me.scaleField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.Xml.Serialization.XmlIncludeAttribute(GetType(voxelentity)),  _
 System.Xml.Serialization.XmlIncludeAttribute(GetType(textentity)),  _
 System.Xml.Serialization.XmlIncludeAttribute(GetType(terrainentity)),  _
 System.Xml.Serialization.XmlIncludeAttribute(GetType(lightentity)),  _
 System.Xml.Serialization.XmlIncludeAttribute(GetType(inputentity)),  _
 System.Xml.Serialization.XmlIncludeAttribute(GetType(buttonentity)),  _
 System.Xml.Serialization.XmlIncludeAttribute(GetType(canvasentity)),  _
 System.Xml.Serialization.XmlIncludeAttribute(GetType(characterentity)),  _
 System.Xml.Serialization.XmlIncludeAttribute(GetType(meshentity)),  _
 System.Xml.Serialization.XmlIncludeAttribute(GetType(containerentity)),  _
 System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class entity
    
    Private transformField As basetransform
    
    Private entity1Field() As entity
    
    Private synchronizerField As String
    
    Private tagField As String
    
    Private idField As String
    
    Private onloadeventField As String
    
    '''<remarks/>
    Public Property transform() As basetransform
        Get
            Return Me.transformField
        End Get
        Set
            Me.transformField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("entity")>  _
    Public Property entity1() As entity()
        Get
            Return Me.entity1Field
        End Get
        Set
            Me.entity1Field = value
        End Set
    End Property
    
    '''<remarks/>
    Public Property synchronizer() As String
        Get
            Return Me.synchronizerField
        End Get
        Set
            Me.synchronizerField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property tag() As String
        Get
            Return Me.tagField
        End Get
        Set
            Me.tagField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property id() As String
        Get
            Return Me.idField
        End Get
        Set
            Me.idField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute("on-load-event")>  _
    Public Property onloadevent() As String
        Get
            Return Me.onloadeventField
        End Get
        Set
            Me.onloadeventField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class voxelentity
    Inherits entity
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class textentity
    Inherits entity
    
    Private textField As String
    
    Private fontsizeField As Single
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property text() As String
        Get
            Return Me.textField
        End Get
        Set
            Me.textField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute("font-size")>  _
    Public Property fontsize() As Single
        Get
            Return Me.fontsizeField
        End Get
        Set
            Me.fontsizeField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class terrainentity
    Inherits entity
    
    Private lengthField As Double
    
    Private widthField As Double
    
    Private heightField As Double
    
    Private heightsField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property length() As Double
        Get
            Return Me.lengthField
        End Get
        Set
            Me.lengthField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property width() As Double
        Get
            Return Me.widthField
        End Get
        Set
            Me.widthField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property height() As Double
        Get
            Return Me.heightField
        End Get
        Set
            Me.heightField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property heights() As String
        Get
            Return Me.heightsField
        End Get
        Set
            Me.heightsField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class lightentity
    Inherits entity
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class inputentity
    Inherits entity
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class buttonentity
    Inherits entity
    
    Private onclickeventField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute("on-click-event")>  _
    Public Property onclickevent() As String
        Get
            Return Me.onclickeventField
        End Get
        Set
            Me.onclickeventField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class canvasentity
    Inherits entity
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class characterentity
    Inherits entity
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class meshentity
    Inherits entity
    
    Private meshnameField As String
    
    Private meshresourceField() As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("mesh-name")>  _
    Public Property meshname() As String
        Get
            Return Me.meshnameField
        End Get
        Set
            Me.meshnameField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("mesh-resource")>  _
    Public Property meshresource() As String()
        Get
            Return Me.meshresourceField
        End Get
        Set
            Me.meshresourceField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class containerentity
    Inherits entity
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class background
    
    Private itemField As String
    
    Private itemElementNameField As ItemChoiceType
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("color", GetType(String)),  _
     System.Xml.Serialization.XmlElementAttribute("panorama", GetType(String)),  _
     System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>  _
    Public Property Item() As String
        Get
            Return Me.itemField
        End Get
        Set
            Me.itemField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlIgnoreAttribute()>  _
    Public Property ItemElementName() As ItemChoiceType
        Get
            Return Me.itemElementNameField
        End Get
        Set
            Me.itemElementNameField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0", IncludeInSchema:=false)>  _
Public Enum ItemChoiceType
    
    '''<remarks/>
    color
    
    '''<remarks/>
    panorama
End Enum

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class synchronizationservice
    
    Private typeField As String
    
    Private addressField As String
    
    Private idField As String
    
    Private sessionField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property type() As String
        Get
            Return Me.typeField
        End Get
        Set
            Me.typeField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property address() As String
        Get
            Return Me.addressField
        End Get
        Set
            Me.addressField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property id() As String
        Get
            Return Me.idField
        End Get
        Set
            Me.idField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()>  _
    Public Property session() As String
        Get
            Return Me.sessionField
        End Get
        Set
            Me.sessionField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://www.fivesqd.com/schemas/veml/1.0")>  _
Partial Public Class vemlEnvironment
    
    Private backgroundField As background
    
    Private entityField() As entity
    
    '''<remarks/>
    Public Property background() As background
        Get
            Return Me.backgroundField
        End Get
        Set
            Me.backgroundField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("entity")>  _
    Public Property entity() As entity()
        Get
            Return Me.entityField
        End Get
        Set
            Me.entityField = value
        End Set
    End Property
End Class
