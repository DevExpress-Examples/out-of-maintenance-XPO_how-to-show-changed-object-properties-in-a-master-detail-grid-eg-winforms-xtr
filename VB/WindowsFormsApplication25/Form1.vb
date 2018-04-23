Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid

Namespace WindowsFormsApplication25
	Partial Public Class Form1
		Inherits Form

		Private uow As UnitOfWork
		Public Sub New()
			InitializeComponent()
			uow = New UnitOfWork()
			Dim xpc As New XPCollection(Of DomainObject)(uow)
			gridControl1.DataSource = xpc

			AddHandler gridView1.RowStyle, AddressOf gridView1_RowStyle
			AddHandler gridControl1.ViewRegistered, AddressOf gridControl1_ViewRegistered
		End Sub

		Private Sub gridControl1_ViewRegistered(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.ViewOperationEventArgs)
			If TypeOf e.View Is GridView Then
				AddHandler (CType(e.View, GridView)).RowStyle, AddressOf Form1_RowStyle
			End If
		End Sub

		Private Sub Form1_RowStyle(ByVal sender As Object, ByVal e As RowStyleEventArgs)
			Dim gridView As GridView = CType(sender, GridView)
			If (Not gridView.IsDataRow(e.RowHandle)) Then
				Return
			End If
			Dim data As PropertyData = TryCast(gridView.GetRow(e.RowHandle), PropertyData)
			If data IsNot Nothing AndAlso (Not Equals(data.NewValue, data.OldValue)) Then
				e.Appearance.BackColor = Color.LightSalmon
			End If
		End Sub

		Private Sub gridView1_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
			Dim gridView As GridView = CType(sender, GridView)
			If (Not gridView.IsDataRow(e.RowHandle)) Then
				Return
			End If
			If uow.IsObjectToSave(gridView.GetRow(e.RowHandle)) Then
				e.Appearance.BackColor = Color.LightSalmon
			End If
		End Sub

		Private Sub gridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles gridControl1.EmbeddedNavigator.ButtonClick
			uow.CommitChanges()
		End Sub
	End Class
End Namespace
