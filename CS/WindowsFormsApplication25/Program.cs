using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Xpo;

namespace WindowsFormsApplication25 {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            //XpoDefault.DataLayer = XpoDefault.GetDataLayer(DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("localhost", "PreviewPropertyChanges"), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            XpoDefault.DataLayer = new SimpleDataLayer(new DevExpress.Xpo.DB.InMemoryDataStore());
            CreateSampleData();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void CreateSampleData() {
            using (UnitOfWork uow = new UnitOfWork()) {
                if (uow.FindObject<DomainObject>(null) == null) {
                    int n = 13;
                    for (int i = 0; i < n; i++) {
                        DomainObject obj = new DomainObject(uow);
                        obj.IntProperty = i;
                        obj.DecimalProperty = (decimal)i / n;
                        obj.StringProperty = string.Format("sample{0}", i);
                        obj.DateTimeProperty = DateTime.Today.AddDays(i);
                    }
                    uow.CommitChanges();
                }
            }
        }
    }
}
