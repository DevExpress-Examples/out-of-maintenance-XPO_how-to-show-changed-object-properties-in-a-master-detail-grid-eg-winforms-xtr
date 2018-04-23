Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.Xpo

Namespace WindowsFormsApplication25
	Friend NotInheritable Class Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		Private Sub New()
		End Sub
		<STAThread> _
		Shared Sub Main()

			'XpoDefault.DataLayer = XpoDefault.GetDataLayer(DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("localhost", "PreviewPropertyChanges"), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
			XpoDefault.DataLayer = New SimpleDataLayer(New DevExpress.Xpo.DB.InMemoryDataStore())
			CreateSampleData()

			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New Form1())
		End Sub

		Private Shared Sub CreateSampleData()
			Using uow As New UnitOfWork()
				If uow.FindObject(Of DomainObject)(Nothing) Is Nothing Then
					Dim n As Integer = 13
					For i As Integer = 0 To n - 1
						Dim obj As New DomainObject(uow)
						obj.IntProperty = i
						obj.DecimalProperty = CDec(i) / n
						obj.StringProperty = String.Format("sample{0}", i)
						obj.DateTimeProperty = DateTime.Today.AddDays(i)
					Next i
					uow.CommitChanges()
				End If
			End Using
		End Sub
	End Class
End Namespace
