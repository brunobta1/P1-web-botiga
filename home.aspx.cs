using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Projecte_1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //private static String[][] carret = new String[50][];

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Cookies.Count == 1)
            {
                totalproductes.InnerText = Request.Cookies.Count + " producte";

            }
            else
            {
                totalproductes.InnerText = Request.Cookies.Count + " productes";
            }

            Page.MaintainScrollPositionOnPostBack = true;

            int cont = 0;

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath(".") + "/productes/");

            FileInfo[] arxius = dir.GetFiles("*.txt");


            foreach (FileInfo file in arxius)
            {
                //Per cada fitxer txt del directori

                if (cont < 4)
                {
                    //Nom del fitxer
                    string nom = file.Name;

                    string nomsimple = nom.Substring(0, nom.Length - 4);
                    int num = Convert.ToInt32(nomsimple.Substring(1, nomsimple.Length-1));

                    string ruta = Server.MapPath(".") + "/productes/";

                    string text = System.IO.File.ReadAllText(ruta + nom);

                    string nomimg = nom.Substring(0, nom.Length - 4) + ".jpg";

                    

                    if(File.Exists(ruta + nomimg))
                    {
                        //Si existeix una foto de producte

                            //Separar la linia pel caracter ; i obtenir totes les dades de l'array
                            string[] arraydades = text.Split(';');

                            if (arraydades.Length == 4)
                            {
                            //Agafem dades del array
                            string textmarca = arraydades[0];
                            string textmodel = arraydades[1];
                            string textdesc = arraydades[2];
                            string textpreu = arraydades[3];

                                if (textmarca.Length > 0 && textmodel.Length > 0 && textdesc.Length > 0 && textpreu.Length > 0)
                                {
                                    //Augmentem el contador de productes
                                    cont++;

                                    //Crear div producte
                                    System.Web.UI.HtmlControls.HtmlGenericControl producte = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                                    producte.ID = "div" + num;
                                    producte.Attributes["class"] = "producte";
                                    productes.Controls.Add(producte);
                                    
                                    //Crear div imgproducte
                                    System.Web.UI.HtmlControls.HtmlGenericControl imgproducte = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                                    imgproducte.Attributes["class"] = "imgproducte";
                                    producte.Controls.Add(imgproducte);
                                    
                                    //Crear imatge
                                    //nom imatge

                                    System.Web.UI.HtmlControls.HtmlGenericControl imatge = new System.Web.UI.HtmlControls.HtmlGenericControl("IMG");
                                    imatge.Attributes["src"] = "productes/" + nomimg;
                                    imatge.ID = "img_" + num;
                                    imgproducte.Controls.Add(imatge);

                                    //Crear div quantitat
                                    System.Web.UI.HtmlControls.HtmlGenericControl divquantitat = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                                    divquantitat.Attributes["class"] = "quantitat";
                                    producte.Controls.Add(divquantitat);

                                    //Crear div quantitat-1
                                    System.Web.UI.HtmlControls.HtmlGenericControl quantitat1 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                                    quantitat1.Attributes["class"] = "quantitat-1";
                                    divquantitat.Controls.Add(quantitat1);

                                    //Crear p
                                    System.Web.UI.HtmlControls.HtmlGenericControl p = new System.Web.UI.HtmlControls.HtmlGenericControl("P");
                                    p.InnerText = "Quantitat: ";
                                    quantitat1.Controls.Add(p);


                                    //Crear textbox
                                    TextBox txt1 = new TextBox();
                                    txt1.ID = "qnt_producte_" + num;
                                    txt1.Text = "0";
                                    txt1.CssClass = "textbox";
                                    txt1.Attributes.Add("onkeydown", "return (!(event.keyCode>=65) && event.keyCode!=32);");
                                    txt1.Attributes.Add("runat", "server");
                                    txt1.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                                    quantitat1.Controls.Add(txt1);


                                    //Crear div quantitat-2
                                    System.Web.UI.HtmlControls.HtmlGenericControl quantitat2 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                                    quantitat2.Attributes["class"] = "quantitat-2";
                                    divquantitat.Controls.Add(quantitat2);

                                    //Crear boto suma
                                    Button btn1 = new Button();
                                    btn1.Attributes.Add("class", "button button_sum");
                                    btn1.ID = "sum_qnt_" + num;
                                    btn1.Click += new EventHandler(Sumar);
                                    btn1.Attributes.Add("runat", "server");
                                    btn1.Text = "+1";
                                    quantitat2.Controls.Add(btn1);

                                    //Crear boto resta
                                    Button btn2 = new Button();
                                    btn2.Attributes.Add("class", "button button_resta");
                                    btn2.ID = "menys_qnt_" + num;
                                    btn2.Click += new EventHandler(Restar);
                                    btn2.Attributes.Add("runat", "server");
                                    btn2.Text = "-1";
                                    quantitat2.Controls.Add(btn2);

                                    //Crear boto confirmar
                                    Button conf = new Button();
                                    conf.Attributes.Add("class", "confirmar");
                                    conf.ID = "confirma_" + num;
                                    conf.Click += new EventHandler(Confirmar);
                                    conf.Attributes.Add("runat", "server");
                                    conf.Text = "Afegir al carret";
                                    producte.Controls.Add(conf);

                                    /*
                                    Label error = new Label();
                                    error.Text = "Especifica una quantitat per afegir el producte al carret";
                                    error.ID = "error" + cont;
                                    error.Visible = false;
                                    error.CssClass = "error";
                                    producte.Controls.Add(error);
                                    */

                                    //Crear div dades
                                    System.Web.UI.HtmlControls.HtmlGenericControl dades = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                                    dades.Attributes["class"] = "dades";
                                    producte.Controls.Add(dades);

                                    //Crear dades
                                    Label marca = new Label();
                                    marca.Text = textmarca;
                                    marca.ID = "marca_" + num;
                                    marca.CssClass = "marca";
                                    dades.Controls.Add(marca);

                                    Label model = new Label();
                                    model.Text = textmodel;
                                    model.ID = "model_" + num;
                                    model.CssClass = "model";
                                    dades.Controls.Add(model);

                                    Label desc = new Label();
                                    desc.Text = textdesc;
                                    desc.ID = "desc_" + num;
                                    dades.Controls.Add(desc);

                                    Label preu = new Label();
                                    preu.Text = textpreu + "€";
                                    preu.ID = "preu_" + num;
                                    preu.CssClass = "preu";
                                    dades.Controls.Add(preu);

                                    Label lblnum = new Label();
                                    lblnum.Text = num.ToString();
                                    lblnum.Visible = false;
                                    lblnum.ID = "codi_" + num;


                                }

                            
                            }

                            
                    }
                        
                    
 
                }
                


            }

            if(cont == 0)
            {
                //Mostrar missatge de que no hi ha productes
                buit.Attributes.Remove("class");
                buit.Attributes.Add("class", "mostrar");
            }


        }

        protected void Sumar(object sender, EventArgs e)
        {
            Button btn1 = sender as Button;
            String idboto = btn1.ID;
            int num = Convert.ToInt32(idboto.Split('_').Last());
            String textbox = "qnt_producte_" + num;

            TextBox tb = (TextBox)Page.FindControl(textbox);
            tb.Text = ((Convert.ToInt32(tb.Text))+1).ToString();
        }

        protected void Restar(object sender, EventArgs e)
        {
            Button btn2 = sender as Button;
            String idboto = btn2.ID;
            int num = Convert.ToInt32(idboto.Split('_').Last());
            String textbox = "qnt_producte_" + num;

            TextBox tb2 = (TextBox)Page.FindControl(textbox);
            int valor = Convert.ToInt32(tb2.Text);
            if(valor > 0)
            {
                tb2.Text = ((Convert.ToInt32(tb2.Text)) + -1).ToString();
            }
            else
            {

            }

        }

        protected void Confirmar(object sender, EventArgs e)
        {
            Button conf = sender as Button;
            String idboto = conf.ID;
            int num = Convert.ToInt32(idboto.Split('_').Last());

            //Agafem la quantitat del TextBox
            String idqnt = "qnt_producte_" + num;
            TextBox tb2 = (TextBox)Page.FindControl(idqnt);
            int qnt = Convert.ToInt32(tb2.Text);

            if(qnt > 0)
            {
                //Agafem la marca del producte
                String idmarca = "marca_" + num;
                Label lblmarca = (Label)Page.FindControl(idmarca);
                String marca = lblmarca.Text;

                //Agafem el model del producte
                String idmodel = "model_" + num;
                Label lblmodel = (Label)Page.FindControl(idmodel);
                String model = lblmodel.Text;

                //Agafem la descripcio del producte
                String iddesc = "desc_" + num;
                Label lbldesc = (Label)Page.FindControl(iddesc);
                String desc = lbldesc.Text;

                //Agafem el preu del producte
                String idpreu = "preu_" + num;
                Label lblpreu = (Label)Page.FindControl(idpreu);
                int preu = Convert.ToInt32(lblpreu.Text.Remove((lblpreu.Text).Length - 1));

                //Agafem la imatge del producte
                String imatge = "productes/p" + num + ".jpg";


                


                //string[] producte = {marca, model, desc, preu.ToString(), qnt.ToString()};

                //String stringprod = marca + ";" + model + ";" + desc + ";" + preu.ToString() + ";" + qnt.ToString() + ",";

                /*
                string[][] carret;

                if (Session["carret"] == null)
                {
                    carret = new string[50][];
                    carret[0] = producte;
                    Session["carret"] = carret;
                }
                else if (Session["carret"] != null)
                {
                    carret = (string[][])Session["carret"];
                    int index = Array.FindIndex(carret, i => i == null);
                    carret[index] = producte;
                    Session["carret"] = carret;

                    prova.Text = carret[0][1];
                }
                
                */

                String nomcookie = marca + model;
                int contcookies = Request.Cookies.Count;
                HttpCookie cookie = Request.Cookies[nomcookie];
                if (cookie == null)
                {
                    //Crear una cookie
                    HttpCookie prod = new HttpCookie(nomcookie);

                    //Posar valors
                    prod.Values.Add("marca", marca);
                    prod.Values.Add("model", model);
                    prod.Values.Add("desc", desc);
                    prod.Values.Add("preu", preu.ToString());
                    prod.Values.Add("qnt", qnt.ToString());
                    prod.Values.Add("num", num.ToString());
                    prod.Values.Add("imatge", imatge);

                    //Posar data de expire
                    prod.Expires = DateTime.Now.AddHours(12);

                    //Afegir la cookie al client
                    Response.Cookies.Add(prod);

                    contcookies++;




                } else if (cookie != null)
                {
                    //Ja existeix un producte a les cookies

                    int novaqnt = Convert.ToInt32(cookie.Values.Get("qnt")) + qnt;

                    //Hem de afegir el producte en una nova cookie
                    HttpCookie prod = new HttpCookie(nomcookie);

                    //Posar valors
                    prod.Values.Add("marca", marca);
                    prod.Values.Add("model", model);
                    prod.Values.Add("desc", desc);
                    prod.Values.Add("preu", preu.ToString());
                    prod.Values.Add("qnt", novaqnt.ToString());
                    prod.Values.Add("num", num.ToString());
                    prod.Values.Add("imatge", imatge);

                    //Posar data de expire
                    prod.Expires = DateTime.Now.AddHours(12);

                    //Afegir la cookie al client
                    Response.Cookies.Add(prod);




                }

                    if (contcookies == 1)
                    {
                        totalproductes.InnerText = contcookies + " producte";
                    }
                    else
                    {
                        totalproductes.InnerText = contcookies + " productes";
                    }

                
                

            }
            else if(qnt == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Alerta", "alert('" + "Selecciona una quantitat per afegir el producte al carret de la compra" + "');", true);

            }

            

 


        }
    }
}