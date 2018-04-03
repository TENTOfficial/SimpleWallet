using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace SimpleWallet
{
    public partial class AddressBook : Form
    {
        public List<Types.AddressBook> book = new List<Types.AddressBook>();
        Api api = Api.Instance;
        public AddressBook(List<Types.AddressBook> book)
        {
            InitializeComponent();
            this.book = new List<Types.AddressBook>(book);
        }

        private void AddressBook_Load(object sender, EventArgs e)
        {
            dtgAddressBook.RowHeadersVisible = false;
            dtgAddressBook.Columns[0].HeaderText = "Label";
            dtgAddressBook.Columns[0].Width = dtgAddressBook.Width * 4 / 10;
            dtgAddressBook.Columns[0].ReadOnly = true;
            dtgAddressBook.Columns[1].HeaderText = "Address";
            dtgAddressBook.Columns[1].Width = dtgAddressBook.Width * 6 / 10;
            dtgAddressBook.Columns[1].ReadOnly = true;

            //populate addressbook
            populateAddressBook(api.readAddressBook());
        }


        void populateAddressBook(List<Types.AddressBook> datas)
        {

            dtgAddressBook.Invoke(new Action(() =>
            {
                dtgAddressBook.AutoGenerateColumns = true;
                dtgAddressBook.DataSource = new BindingList<Types.AddressBook>(datas);
            }));
        }

        void addAddressBook(Types.AddressBook input)
        {
            String label = input.label;

            DataGridViewRow row = new DataGridViewRow();
            try
            {
                row = dtgAddressBook.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[0].Value.ToString().Equals(label))
                .First();

                MessageBox.Show("Duplicated label");
            }
            catch (Exception ex)
            {
                try
                {
                    book.Add(new Types.AddressBook(label, input.address));
                    saveAddressBook(book);
                    
                    book.Sort(delegate(Types.AddressBook x, Types.AddressBook y)
                    {
                        return x.label.CompareTo(y.label);
                    });
                    dtgAddressBook.Invoke(new Action(() =>
                    {
                        dtgAddressBook.AutoGenerateColumns = true;
                        dtgAddressBook.DataSource = new BindingList<Types.AddressBook>(book);
                    }));
                }
                catch (Exception ex1)
                {
                    //if (shouldRestart)
                    //{
                    //    return;
                    //}
                    //addTextToRtb(rtbError, "Exception: " + ex.Message + "\n");
                }
            }
        }

        void editAddressBook(Types.AddressBook input, DataGridView dtg, int index, string oldLabel)
        {
            try
            {
                DataGridViewRow row = new DataGridViewRow();
                row = dtgAddressBook.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[0].Value.ToString().Equals(input.label))
                .First();

                if (row.Index != index)
                {
                    MessageBox.Show("Duplicated label");
                    return;
                }
            }
            catch (Exception ex)
            {
            }
            book.RemoveAll(x => x.label == oldLabel);
            book.Add(new Types.AddressBook(input.label, input.address));
            book.Sort(delegate(Types.AddressBook x, Types.AddressBook y)
            {
                return x.label.CompareTo(y.label);
            });
            dtgAddressBook.Invoke(new Action(() =>
            {
                dtgAddressBook.AutoGenerateColumns = true;
                dtgAddressBook.DataSource = new BindingList<Types.AddressBook>(book);
            }));
            saveAddressBook(book);
        }

        void saveAddressBook(List<Types.AddressBook> addr)
        {
            addr.Sort(delegate(Types.AddressBook x, Types.AddressBook y)
            {
                return x.label.CompareTo(y.label);
            });
            Dictionary<String, List<Types.AddressBook>> jsonData = new Dictionary<String, List<Types.AddressBook>>();
            jsonData["addressbook"] = addr;
            String json = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
            File.WriteAllText(Types.addressLabel, json);
        }

        private void btnNewAddressBook_Click(object sender, EventArgs e)
        {
            NewLabel newLb = new NewLabel();
            newLb.ShowDialog();
            if (newLb.edit)
            {
                String name = newLb.name;
                String address = newLb.address;
                addAddressBook(new Types.AddressBook(name, address));
            }
        }

        private void dtgAddressBook_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Types.CtxMenuType type = Types.CtxMenuType.MASTERNODE;
                ContextMenu ctxMenu = new ContextMenu();
                if (((DataGridView)sender).Name == "dtgAddressBook")
                {
                    type = Types.CtxMenuType.ADDRESS_BOOK;
                }
                ctxMenu.MenuItems.Add(new CustomMenuItem("Copy", ctxMenu_Copy, type));
                ctxMenu.MenuItems.Add(new CustomMenuItem("Edit", ctxMenu_Edit, type));
                ctxMenu.MenuItems.Add(new CustomMenuItem("Delete", ctxMenu_Delete, type));

                int currentMouseOverRow = ((DataGridView)sender).HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverColumn = ((DataGridView)sender).HitTest(e.X, e.Y).ColumnIndex;
                if (currentMouseOverRow >= 0 && currentMouseOverColumn >= 0)
                {
                    ((DataGridView)sender).CurrentCell = ((DataGridView)sender).Rows[currentMouseOverRow].Cells[currentMouseOverColumn];
                }

                ctxMenu.Show(((DataGridView)sender), new Point(e.X, e.Y));

            }
        }

        private void ctxMenu_Copy(Object sender, System.EventArgs e)
        {
            CustomMenuItem item = sender as CustomMenuItem;
            if (item.type == Types.CtxMenuType.ADDRESS_BOOK)
            {
                if (dtgAddressBook.CurrentCell != null && dtgAddressBook.CurrentCell.Value != null)
                {
                    String text = dtgAddressBook.CurrentCell.Value.ToString();
                    Clipboard.SetText(text);
                }
            }
        }

        private void ctxMenu_Edit(Object sender, System.EventArgs e)
        {
            CustomMenuItem item = sender as CustomMenuItem;

            if (item.type == Types.CtxMenuType.ADDRESS_BOOK)
            {
                DataGridView dtg = dtgAddressBook;
                int index = dtg.CurrentRow.Index;
                String label = dtg.Rows[index].Cells[0].Value.ToString();
                String address = dtg.Rows[index].Cells[1].Value.ToString();
                NewLabel newLb = new NewLabel(label, address);
                newLb.ShowDialog();
                if (newLb.edit)
                {
                    editAddressBook(new Types.AddressBook(newLb.name, newLb.address), dtg, index, label);
                }
            }
        }

        private void ctxMenu_Delete(Object sender, System.EventArgs e)
        {
            CustomMenuItem item = sender as CustomMenuItem;
            if (item.type == Types.CtxMenuType.ADDRESS_BOOK)
            {
                try
                {
                    if (dtgAddressBook.CurrentCell != null && dtgAddressBook.Rows[dtgAddressBook.CurrentCell.RowIndex].Cells[0].Value != null)
                    {
                        String name = dtgAddressBook.Rows[dtgAddressBook.CurrentCell.RowIndex].Cells[0].Value.ToString();
                        if (!String.IsNullOrEmpty(name))
                        {
                            DialogResult dialogResult = MessageBox.Show(@"Do you want to delete " + name, "ATTENTION!!!", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                int index = book.FindIndex(x => x.label == name);
                                if (index >= 0)
                                {
                                    book.RemoveAt(index);
                                }
                                book.Sort(delegate(Types.AddressBook x, Types.AddressBook y)
                                {
                                    return x.label.CompareTo(y.label);
                                });
                                dtgAddressBook.Invoke(new Action(() =>
                                {
                                    dtgAddressBook.AutoGenerateColumns = true;
                                    dtgAddressBook.DataSource = new BindingList<Types.AddressBook>(book);
                                }));
                                saveAddressBook(book);
                                //populate addressbook
                                populateAddressBook(book);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete empty String");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete label error");
                }
            }
        }
    }
}
