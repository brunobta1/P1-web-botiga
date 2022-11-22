using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace Projecte_1
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;

            //Ficar el numero de elements trobats a les cookies
            numelements.Text = "Elements: " + (Request.Cookies.Count);

            totalelements.Text = Request.Cookies.Count + " elements";

            //Crear taula i capçalera amb titols
            Table t = new Table();
            t.ID = "taula";
            divtaula.Controls.Add(t);

            //Crear row de titols
            TableHeaderRow thr = new TableHeaderRow();
            t.Controls.Add(thr);

            //Crear titol 1
            TableHeaderCell hc1 = new TableHeaderCell();
            hc1.Text = "Detalls del producte";
            thr.Controls.Add(hc1);

            //Crear titol 2
            TableHeaderCell hc2 = new TableHeaderCell();
            hc2.Text = "Quantitat";
            thr.Controls.Add(hc2);

            //Crear titol 3
            TableHeaderCell hc3 = new TableHeaderCell();
            hc3.Text = "Preu";
            thr.Controls.Add(hc3);

            //Crear titol 4
            TableHeaderCell hc4 = new TableHeaderCell();
            hc4.Text = "Total";
            thr.Controls.Add(hc4);

            totalpreu.Text = "0";
            int totalcarret = 0;
            int elementstotals = 0;
            if (Request.Cookies.Count == 0)
            {
                carret.Visible = false;
                carretbuit.Visible = true;
            }
            else
            {
                
                for (int i = 0; i < Request.Cookies.Count; i++)
                {
                    //Per cada producte que troba a les cookies
                    String marca = Request.Cookies[i].Values.Get("marca");
                    String model = Request.Cookies[i].Values.Get("model");
                    String desc = Request.Cookies[i].Values.Get("desc");
                    int preu = Convert.ToInt32(Request.Cookies[i].Values.Get("preu"));
                    int qnt = Convert.ToInt32(Request.Cookies[i].Values.Get("qnt"));
                    int num = Convert.ToInt32(Request.Cookies[i].Values.Get("num"));
                    String nomimg = Request.Cookies[i].Values.Get("imatge");
                    int preutotal = preu * qnt;
                    elementstotals = elementstotals + qnt;

                    //Crear row amb dades del producte
                    TableRow tr = new TableRow();
                    t.Controls.Add(tr);

                    //Crear tablecell
                    TableCell tc = new TableCell();
                    tr.Controls.Add(tc);

                    //Crear div detalls
                    System.Web.UI.HtmlControls.HtmlGenericControl detalls = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    detalls.Attributes["class"] = "detalls";
                    tc.Controls.Add(detalls);

                    //Crear imatge
                    System.Web.UI.HtmlControls.HtmlGenericControl imatge = new System.Web.UI.HtmlControls.HtmlGenericControl("IMG");
                    imatge.Attributes["src"] = nomimg;
                    detalls.Controls.Add(imatge);

                    //Crear detalls-dreta
                    System.Web.UI.HtmlControls.HtmlGenericControl detallsdreta = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    detallsdreta.Attributes["class"] = "detalls-dreta";
                    detalls.Controls.Add(detallsdreta);

                    //Crear Label amb la marca
                    Label lblmarca = new Label();
                    lblmarca.Attributes["class"] = "marcacarret";
                    lblmarca.Text = marca;
                    detallsdreta.Controls.Add(lblmarca);

                    //Crear label amb el model i descripcio
                    Label lblinfo = new Label();
                    lblinfo.Attributes["class"] = "modelcarret";
                    lblinfo.Text = model + " " + desc;
                    detallsdreta.Controls.Add(lblinfo);

                    //Crear boto eliminar
                    Button elim = new Button();
                    elim.Attributes.Add("class", "eliminar");
                    elim.ID = "elimina_" + num;
                    elim.Click += new EventHandler(Eliminar);
                    elim.Attributes.Add("runat", "server");
                    elim.Text = "Eliminar";
                    detallsdreta.Controls.Add(elim);

                    //Crear tablecell
                    TableCell quantitat = new TableCell();
                    tr.Controls.Add(quantitat);

                    //Crear div quantitat carret
                    System.Web.UI.HtmlControls.HtmlGenericControl quantitatcarret = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    quantitatcarret.Attributes["class"] = "quantitat-carret";
                    quantitat.Controls.Add(quantitatcarret);

                    System.Web.UI.HtmlControls.HtmlGenericControl quantitat_top = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    quantitat_top.Attributes["class"] = "quantitat-top";
                    quantitatcarret.Controls.Add(quantitat_top);

                    //Crear boto restar
                    Button rest = new Button();
                    rest.Attributes.Add("class", "btn-qnt");
                    rest.ID = "resta_" + num;
                    rest.Click += new EventHandler(Restar);
                    rest.Attributes.Add("runat", "server");
                    rest.Text = "-";
                    quantitat_top.Controls.Add(rest);

                    //Crear textbox
                    TextBox txt1 = new TextBox();
                    txt1.ID = "qnt_producte_" + num;
                    txt1.Text = qnt.ToString();
                    txt1.CssClass = "qnt-carret";
                    txt1.Attributes.Add("onkeydown", "return (!(event.keyCode>=65) && event.keyCode!=32);");
                    txt1.Attributes.Add("runat", "server");
                    txt1.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    quantitat_top.Controls.Add(txt1);

                    //Crear boto sumar
                    Button sum = new Button();
                    sum.Attributes.Add("class", "btn-qnt");
                    sum.ID = "suma_" + num;
                    sum.Click += new EventHandler(Sumar);
                    sum.Attributes.Add("runat", "server");
                    sum.Text = "+";
                    quantitat_top.Controls.Add(sum);

                    //Crear boto de confirmar la quantitat
                    Button confirma = new Button();
                    confirma.Attributes.Add("class", "botoconfirmar");
                    confirma.ID = "confirma_" + num;
                    confirma.Click += new EventHandler(Confirma);
                    confirma.Attributes.Add("runat", "server");
                    confirma.Text = "Confirmar";
                    quantitatcarret.Controls.Add(confirma);


                    //Crear cell de preu
                    TableCell cellpreu = new TableCell();
                    tr.Controls.Add(cellpreu);

                    //Crear label amb el preu
                    Label lblpreu = new Label();
                    lblpreu.Text = preu + "€";
                    lblpreu.ID = "preu_" + num;
                    cellpreu.Controls.Add(lblpreu);

                    //Crear cell del preu total
                    TableCell cellpreutotal = new TableCell();
                    tr.Controls.Add(cellpreutotal);

                    //Crear label amb el preu total
                    Label lblpreutotal = new Label();
                    lblpreutotal.Text = (preu * qnt) + "€";
                    lblpreutotal.ID = "preutotal_" + num;
                    cellpreutotal.Controls.Add(lblpreutotal);

                    totalcarret = totalcarret + preutotal;

                }
                    totalelements.Text = elementstotals + " elements";
                    totalpreu.Text = totalcarret + "€";
            }
            
        }


        protected void Eliminar(object sender, EventArgs e)
        {
            //Obtenim el numero del boto
            Button conf = sender as Button;
            String idboto = conf.ID;
            int num = Convert.ToInt32(idboto.Split('_').Last());

            for (int i = 0; i < Request.Cookies.Count; i++)
            {
                int numprod = Convert.ToInt32(Request.Cookies[i].Values.Get("num"));
                if (numprod == num)
                {
                    string nomcookie = Request.Cookies[i].Name;
                    Response.Cookies[nomcookie].Expires = DateTime.Now.AddDays(-1);
                }
            }
            Page.Response.Redirect("carret.aspx");
        }

        
        protected void Comanda(object sender, EventArgs e)
        {
            dadesclient.Visible = true;
        }
        

        protected void Confirma(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            String idboto = btn.ID;
            int num = Convert.ToInt32(idboto.Split('_').Last());
            String textbox = "qnt_producte_" + num;

            //quantitat nova
            TextBox tb2 = (TextBox)Page.FindControl(textbox);
            string valor = tb2.Text;

            //Actualitzar cookie
            string nomcookie = "";
            string marca = "";
            string model = "";
            string desc = "";
            string preu = "";
            string imatge = "";
            int qnt = 0;

            for (int i=0; i < Request.Cookies.Count; i++)
            {
                int numcookie = Convert.ToInt32(Request.Cookies[i].Values.Get("num"));
                if(numcookie == num)
                {
                    nomcookie = Request.Cookies[i].Name;
                    marca = Request.Cookies[i].Values.Get("marca");
                    model = Request.Cookies[i].Values.Get("model");
                    desc = Request.Cookies[i].Values.Get("desc");
                    preu = Request.Cookies[i].Values.Get("preu");
                    imatge = Request.Cookies[i].Values.Get("imatge");
                    qnt = Convert.ToInt32(Request.Cookies[i].Values.Get("qnt"));

                }
            }

            //Hem de afegir el producte en una nova cookie
            HttpCookie prod = new HttpCookie(nomcookie);

            //Posar valors
            prod.Values.Add("marca", marca);
            prod.Values.Add("model", model);
            prod.Values.Add("desc", desc);
            prod.Values.Add("preu", preu);
            prod.Values.Add("qnt", valor);
            prod.Values.Add("num", num.ToString());
            prod.Values.Add("imatge", imatge);

            //Posar data de expire
            prod.Expires = DateTime.Now.AddHours(12);

            //Afegir la cookie al client
            Response.Cookies.Add(prod);

            Response.Redirect("carret.aspx");

        }

        protected void continuar_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void crearComanda(object sender, EventArgs e)
        {
            if (nif.Text.Equals(""))
            {
                errornif.Visible = true;
            }
            else
            {
                errornif.Visible = false;
            }

            if (nom.Text.Equals(""))
            {
                errornom.Visible = true;
            }
            else
            {
                errornom.Visible = false;
            }
            if (telf.Text.Equals(""))
            {
                errortel.Visible = true;
            }
            else
            {
                errortel.Visible = false;
            }

            if (!nif.Text.Equals("") && !nom.Text.Equals("") && !telf.Text.Equals(""))
            {
                DateTime data = DateTime.Now;
                string nomarxiu = Server.MapPath(".") + "/Comandes/" + nif.Text + "_" + data.Year + data.Month + data.Day + ".txt";
                if (File.Exists(nomarxiu))
                {
                    //Ha fet una comanda
                    

                    string nomarxiu2 = nomarxiu.Substring(0, nomarxiu.Length-4) + "_2.txt";
                    



                    if (File.Exists(nomarxiu2))
                    {
                        //Ha fet dues comandes
                        comandacorrecta.Visible = false;
                        errorcomanda.Visible = true;
                    }
                    else
                    {
                        errorcomanda.Visible = false;
                        using (StreamWriter sw = File.CreateText(nomarxiu2))
                        {
                            int importtotal = 0;
                            sw.WriteLine("Dades del client:");
                            sw.WriteLine("Nom: " + nom.Text + " NIF: " + nif.Text + " Telefon: " + telf.Text);
                            sw.WriteLine("==================================================");
                            sw.WriteLine("Elements totals: " + Request.Cookies.Count);
                            for (int i = 0; i < Request.Cookies.Count; i++)
                            {
                                int qnt = Convert.ToInt32(Request.Cookies[i].Values.Get("qnt"));
                                int preu = Convert.ToInt32(Request.Cookies[i].Values.Get("preu"));
                                importtotal += qnt * preu;


                            }
                            sw.WriteLine("Import total: " + importtotal + "€\n");

                            for (int i = 0; i < Request.Cookies.Count; i++)
                            {

                                string marca = Request.Cookies[i].Values.Get("marca");
                                string model = Request.Cookies[i].Values.Get("model");
                                string desc = Request.Cookies[i].Values.Get("desc");
                                int qnt = Convert.ToInt32(Request.Cookies[i].Values.Get("qnt"));
                                int preu = Convert.ToInt32(Request.Cookies[i].Values.Get("preu"));
                                int totalproducte = qnt * preu;
                                int numprod = i + 1;
                                sw.WriteLine("Producte " + numprod + ":");
                                sw.WriteLine(marca + " " + model + " " + desc + "\nUnitats: " + qnt + " Preu Unitari: " + preu + "€      Total: " + totalproducte + "€\n");


                            }
                        }
                        comandacorrecta.Visible = true;
                    }
                    nif.Text = "";
                    nom.Text = "";
                    telf.Text = "";
                }
                else
                {
                    //No ha fet cap comanda
                    errorcomanda.Visible = false;
                    using (StreamWriter sw = File.CreateText(nomarxiu)) {
                        int importtotal = 0;
                        sw.WriteLine("Dades del client:");
                        sw.WriteLine("Nom: " + nom.Text + " NIF: " + nif.Text + " Telefon: " + telf.Text);
                        sw.WriteLine("==================================================");
                        sw.WriteLine("Elements totals: " + Request.Cookies.Count);
                        for (int i = 0; i < Request.Cookies.Count; i++)
                        {
                            int qnt = Convert.ToInt32(Request.Cookies[i].Values.Get("qnt"));
                            int preu = Convert.ToInt32(Request.Cookies[i].Values.Get("preu"));
                            importtotal += qnt * preu;


                        }
                        sw.WriteLine("Import total: " + importtotal + "€\n");

                        for (int i = 0; i < Request.Cookies.Count; i++)
                        {

                        string marca = Request.Cookies[i].Values.Get("marca");
                        string model = Request.Cookies[i].Values.Get("model");
                        string desc = Request.Cookies[i].Values.Get("desc");
                        int qnt = Convert.ToInt32(Request.Cookies[i].Values.Get("qnt"));
                        int preu = Convert.ToInt32(Request.Cookies[i].Values.Get("preu"));
                        int totalproducte = qnt * preu;
                        int numprod = i + 1;
                        sw.WriteLine("Producte " + numprod + ":");
                        sw.WriteLine(marca + " " + model + " " + desc + "\nUnitats: " + qnt + " Preu Unitari: " + preu + "€      Total: " + totalproducte + "€\n");


                        }
                    }
                    nif.Text = "";
                    nom.Text = "";
                    telf.Text = "";
                    comandacorrecta.Visible = true;

                }
            }
        }

        protected void Restar(object sender, EventArgs e)
        {
            Button btn2 = sender as Button;
            String idboto = btn2.ID;
            int num = Convert.ToInt32(idboto.Split('_').Last());
            String textbox = "qnt_producte_" + num;

            TextBox tb2 = (TextBox)Page.FindControl(textbox);
            int valor = Convert.ToInt32(tb2.Text);
            if (valor > 1)
            {
                tb2.Text = ((Convert.ToInt32(tb2.Text)) + -1).ToString();
            }
            else
            {

            }

        }

        protected void Sumar(object sender, EventArgs e)
        {
                Button btn1 = sender as Button;
                String idboto = btn1.ID;
                int num = Convert.ToInt32(idboto.Split('_').Last());
                String textbox = "qnt_producte_" + num;

                TextBox tb = (TextBox)Page.FindControl(textbox);
                tb.Text = ((Convert.ToInt32(tb.Text)) + 1).ToString();
            
        }

        protected void tancarMissatge(object sender, EventArgs e)
        {
            errorcomanda.Visible = false;
            comandacorrecta.Visible = false;
        }

        }
}