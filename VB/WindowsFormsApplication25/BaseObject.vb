Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata

Namespace WindowsFormsApplication25

	Public Class PropertyData
		Private owner As BaseObject
		Private member As XPMemberInfo
		Public Sub New(ByVal owner As BaseObject, ByVal member As XPMemberInfo)
			Me.owner = owner
			Me.member = member
		End Sub
		Public ReadOnly Property Name() As String
			Get
				Return member.Name
			End Get
		End Property
		Public ReadOnly Property Type() As Type
			Get
				Return member.MemberType
			End Get
		End Property
		Public ReadOnly Property OldValue() As Object
			Get
				Return owner.GetOldValue(member)
			End Get
		End Property
		Public ReadOnly Property NewValue() As Object
			Get
				Return member.GetValue(owner)
			End Get
		End Property
	End Class
	<NonPersistent> _
	Public Class BaseObject
		Inherits XPBaseObject
		Public Sub New(ByVal s As Session)
			MyBase.New(s)
			oldValues = New Dictionary(Of XPMemberInfo, Object)()
			properties_Renamed = New List(Of PropertyData)()
			For Each member As XPMemberInfo In Me.ClassInfo.PersistentProperties
				properties_Renamed.Add(New PropertyData(Me, member))
			Next member
		End Sub
		Private oldValues As Dictionary(Of XPMemberInfo, Object)
		Friend Function GetOldValue(ByVal member As XPMemberInfo) As Object
			Dim v As Object
			If oldValues.TryGetValue(member, v) Then
				Return v
			End If
			Return Nothing
		End Function
		Private Sub SaveOldValues()
			oldValues.Clear()
			For Each member As XPMemberInfo In Me.ClassInfo.PersistentProperties
				oldValues.Add(member, member.GetValue(Me))
			Next member
		End Sub
		Protected Overrides Sub OnLoaded()
			MyBase.OnLoaded()
			If Session.IsObjectToSave(Me) Then
				Return
			End If
			SaveOldValues()
		End Sub
		Protected Overrides Sub OnSaved()
			MyBase.OnSaved()
			SaveOldValues()
		End Sub
		Private properties_Renamed As List(Of PropertyData)
		Public ReadOnly Property Properties() As List(Of PropertyData)
			Get
				Return properties_Renamed
			End Get
		End Property
		Public ReadOnly Property Type() As Type
			Get
				Return Me.GetType()
			End Get
		End Property
	End Class
End Namespace
