using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ContadorPaginaPDF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }
        public void ChooseFolder()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            ChooseFolder();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            List<ShowPDFCountPage> showPDFCountPages = new List<ShowPDFCountPage>();
            List<int> CountPages = new List<int>();

            string path = textBox1.Text;

            if (path.Length != 0)
            {
                string[] files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    try
                    {
                        FileInfo fileTracted = new FileInfo(file);
                        var name = fileTracted.Name;

                        if (fileTracted.Extension.ToLower() == ".pdf")
                        {
                            PdfReader pdfReader = new PdfReader(fileTracted.ToString());
                            int qtdePaginas = pdfReader.NumberOfPages;

                            showPDFCountPages.Add(new ShowPDFCountPage(name, qtdePaginas));
                            CountPages.Add(qtdePaginas);
                        }
                       
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Erro ao contar páginas");
                    }
                }

                var colums = new List<(string, string)>
                    {("Arquivo","Arquivo"),
                    ("Quantidade de Páginas","Quantidade de Páginas")};

                colums.ForEach(column => dataGridView1.Columns.Add(column.Item1, column.Item2));
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                showPDFCountPages.ForEach(pdf => dataGridView1.Rows.Add(pdf.File, pdf.CountPage));

                var c = 0;
                foreach (var item in CountPages)
                {
                    c += item;
                }

                label2.Text = $"Páginas em arquivos pdf contidas nesse diretótio: {c}";
            }
            else
            {
                MessageBox.Show("Informe um diretório valido");
            }

        }
    }

    class ShowPDFCountPage
    {
        public string File { get; set; }
        public int CountPage { get; set; }

        public ShowPDFCountPage(string file, int countPage)
        {
            File = file;
            CountPage = countPage;
        }
    }
}
