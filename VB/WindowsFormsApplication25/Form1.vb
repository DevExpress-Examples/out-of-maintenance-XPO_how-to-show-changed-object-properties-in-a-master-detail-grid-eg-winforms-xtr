Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid

Namespace WindowsFormsApplication25

    Public Partial Class Form1
        Inherits Form

        Private uow As UnitOfWork

        Public Sub New()
            InitializeComponent()
            uow = New UnitOfWork()
            Dim xpc As XPCollection(Of DomainObject) = New XPCollection(Of DomainObject)(uow)
            gridControl1.DataSource = xpc
            AddHandler gridView1.RowStyle, New RowStyleEventHandler(AddressOf gridView1_RowStyle)
            AddHandler gridControl1.ViewRegistered, New DevExpress.XtraGrid.ViewOperationEventHandler(AddressOf gridControl1_ViewRegistered)
        End Sub

        Private Sub gridControl1_ViewRegistered(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.ViewOperationEventArgs)
            If TypeOf e.View Is GridView Then
                AddHandler CType(e.View, GridView).RowStyle, New RowStyleEventHandler(AddressOf Form1_RowStyle)
            End If
        End Sub

        Private Sub Form1_RowStyle(ByVal sender As Object, ByVal e As RowStyleEventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not gridView.IsDataRow(e.RowHandle) Then Return
            Dim data As PropertyData = TryCast(gridView.GetRow(e.RowHandle), PropertyData)
            If data IsNot Nothing AndAlso Not Equals(data.NewValue, data.OldValue) Then
                e.Appearance.BackColor = Color.LightSalmon
            End If
        End Sub

        Private Sub gridView1_RowStyle(ByVal sender As Object, ByVal e As RowStyleEventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not gridView.IsDataRow(e.RowHandle) Then Return
            If uow.IsObjectToSave(gridView.GetRow(e.RowHandle)) Then
                e.Appearance.BackColor = Color.LightSalmon
            End If
        End Sub

        Private Sub gridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs)
            uow.CommitChanges()
        End Sub
    End Class
End Namespace
