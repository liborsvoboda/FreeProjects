﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Tento kód byl generován nástrojem.
'     Verze modulu runtime:4.0.30319.34209
'
'     Změny tohoto souboru mohou způsobit nesprávné chování a budou ztraceny,
'     dojde-li k novému generování kódu.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'Tato třída byla automaticky generována třídou StronglyTypedResourceBuilder
    'pomocí nástroje podobného aplikaci ResGen nebo Visual Studio.
    'Chcete-li přidat nebo odebrat člena, upravte souboru .ResX a pak znovu spusťte aplikaci ResGen
    's parametrem /str nebo znovu sestavte projekt aplikace Visual Studio.
    '''<summary>
    '''  Třída prostředků se silnými typy pro vyhledávání lokalizovaných řetězců atp.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Vrací instanci ResourceManager uloženou v mezipaměti použitou touto třídou.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("SESSION_MANAGER.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Potlačí vlastnost CurrentUICulture aktuálního vlákna pro všechna
        '''  vyhledání prostředků pomocí třídy prostředků se silnými typy.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
    End Module
End Namespace