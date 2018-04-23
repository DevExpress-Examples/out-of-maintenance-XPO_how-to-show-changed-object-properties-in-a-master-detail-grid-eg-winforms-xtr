using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.XtraGrid.Views.Grid;

namespace WindowsFormsApplication25 {
    public partial class Form1 : Form {

        private UnitOfWork uow;
        public Form1() {
            InitializeComponent();
            uow = new UnitOfWork();
            XPCollection<DomainObject> xpc = new XPCollection<DomainObject>(uow);
            gridControl1.DataSource = xpc;

            gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridView1_RowStyle);
            gridControl1.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(gridControl1_ViewRegistered);
        }

        void gridControl1_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e) {
            if (e.View is GridView) {
                ((GridView)e.View).RowStyle += new RowStyleEventHandler(Form1_RowStyle);
            }
        }

        void Form1_RowStyle(object sender, RowStyleEventArgs e) {
            GridView gridView = (GridView)sender;
            if (!gridView.IsDataRow(e.RowHandle)) return;
            PropertyData data = gridView.GetRow(e.RowHandle) as PropertyData;
            if (data != null && !Equals(data.NewValue, data.OldValue)) {
                e.Appearance.BackColor = Color.LightSalmon;
            }
        }

        void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) {
            GridView gridView = (GridView)sender;
            if (!gridView.IsDataRow(e.RowHandle)) return;
            if (uow.IsObjectToSave(gridView.GetRow(e.RowHandle))) {
                e.Appearance.BackColor = Color.LightSalmon;
            }
        }

        private void gridControl1_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e) {
            uow.CommitChanges();
        }
    }
}
