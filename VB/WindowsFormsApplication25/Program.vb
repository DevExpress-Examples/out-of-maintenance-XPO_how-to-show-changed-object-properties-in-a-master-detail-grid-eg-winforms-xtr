Imports System
Imports System.Windows.Forms
Imports DevExpress.Xpo

Namespace WindowsFormsApplication25

    Friend Module Program

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Sub Main()
            'XpoDefault.DataLayer = XpoDefault.GetDataLayer(DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("localhost", "PreviewPropertyChanges"), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            XpoDefault.DataLayer = New SimpleDataLayer(New DB.InMemoryDataStore())
            Call CreateSampleData()
            Call Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Call Application.Run(New Form1())
        End Sub

        Private Sub CreateSampleData()
            Using uow As UnitOfWork = New UnitOfWork()
                If uow.FindObject(Of DomainObject)(Nothing) Is Nothing Then
                    Dim n As Integer = 13
                    For i As Integer = 0 To n - 1
                        Dim obj As DomainObject = New DomainObject(uow)
                        obj.IntProperty = i
                        obj.DecimalProperty = CDec(i) / n
                        obj.StringProperty = String.Format("sample{0}", i)
                        obj.DateTimeProperty = Date.Today.AddDays(i)
                    Next

                    uow.CommitChanges()
                End If
            End Using
        End Sub
    End Module
End Namespace
