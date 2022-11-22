<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="carret.aspx.cs" Inherits="Projecte_1.WebForm2" %>
<add key="ValidationSettings:UnobtrusiveValidationMode" value="None" />

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Viplay</title>
    <link rel="icon" href="imatges/icon.png">

    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Prompt&family=Roboto+Condensed&display=swap" rel="stylesheet">

    <link rel="stylesheet" href="css/style.css"/>
    <link rel="stylesheet" href="css/estils.css"/>
</head>
<body id="body-carrito">
    <form id="form1" runat="server">
        <div>
            <header>
            <div id="titol">
                <a href="home.aspx"><img src="imatges/viplay_panoramic-modified.png" /></a>
                <a href="home.aspx"><h1>Viplay</h1></a>
            </div>
            <div id="links">
                <a href="home.aspx"><h3>Inici</h3></a>
                <a href="#stop"><h3>Productes</h3></a>
                <a href=""><h3>Sobre nosaltres</h3></a>
                <a href=""><h3>Contacte</h3></a>
                <a href=""><h3>Blog</h3></a>
            </div>
            <a href="home.aspx" class="icono_home"><span class="icon-home-outline"></span></a>
        </header>
            <div class="carretbuit" id="carretbuit" runat="server" visible="false">
                <h1>El teu carret de la compra està buit</h1>
                <a href="home.aspx">Ves a la botiga</a>
            </div>
                <div id="errorcomanda" visible="false" runat="server">
                    <h2>No s'ha pogut enviar la comanda. Avui ja has fet 2 comandes.</h2>
                    <asp:Button ID="bototancar" class="tancar" runat="server" Text="Tancar" onclick="tancarMissatge"/>
                </div>
            <div id="comandacorrecta" visible="false" runat="server">
                <h2>S'ha enviat la comanda correctament</h2>
                <asp:Button ID="Button1" class="tancar" runat="server" Text="Tancar" onclick="tancarMissatge"/>
            </div>

            <div id="carret" runat="server">
                <h1>El teu carret de la compra</h1>
                <div id="main">
                        <div class="taula" id="divtaula" runat="server">
                            <div id="info-carret">
                                <h2>Carret de la compra</h2>
                                <asp:Label class="numelements" ID="numelements" runat="server"></asp:Label>
                            </div>
                            
                    </div>
                
                    
                <div id="compra">
                    <div id="compra-top">
                        
                        <h2>Total del carret</h2>
                    </div>
                    <div id="compra-bottom">
                        
                        <div id="totalcarret">
                            <div id="totalcarret-top">
                                <asp:Label class="totalelements" ID="totalelements" runat="server"></asp:Label>
                                <asp:Label class="totalpreu" ID="totalpreu" runat="server">0</asp:Label>
                            </div>
                            <div id="totalcarret-bottom">
                                <asp:Button ID="continuar" class="continuar" runat="server" Text="Continuar comprant" OnClick="continuar_Click"/>
                                <div id="realitzar-com">
                                    <asp:Button ID="comprar" class="comprar" runat="server" Text="Realitzar comanda" OnClick="Comanda" />
                                    <div id="dadesclient" runat="server" Visible="false">
                                        <h2>Introdueix les teves dades</h2>
                                        <h3>NIF</h3>
                                        <asp:TextBox id="nif" class="textbox-client" placeholder="NIF" runat="server"></asp:TextBox>
                                        <p id="errornif" runat="server" visible="false">*Aquest camp és obligatori</p>
                                        <h3>Nom</h3>
                                        <asp:TextBox id="nom" class="textbox-client" placeholder="Nom" runat="server"></asp:TextBox>
                                        <p id="errornom" runat="server" visible="false">*Aquest camp és obligatori</p>
                                        <h3>Número de telèfon</h3>
                                        <asp:TextBox id="telf" class="textbox-client" placeholder="Telèfon" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" runat="server"></asp:TextBox>
                                        <p id="errortel" runat="server" visible="false">*Aquest camp és obligatori</p>
                                        <asp:Button ID="confirmar" class="confirmar" runat="server" Text="Confirmar" onclick="crearComanda"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                
                
                
            </div>

            <footer>
            <div class="links_footer">
                <div class="col_1">
                    <a href=""><h2>Inici</h2></a>
                    <a href=""><h2>El teu carret de la compra</h2></a>
                </div>
                <div class="col_2">
                    <a href=""><h2>Productes</h2></a>
                    <a href=""><h2>Avís legal</h2></a>
                </div>
                <div class="col_3">
                    <a href=""><h2>Sobre nosaltres</h2></a>
                    <a href=""><h2>Publicitat</h2></a>
                </div>
                <div class="col_4">
                    <a href=""><h2>Contacte</h2></a>
                    <a href=""><h2>Blog</h2></a>
                </div>
                
            </div>
            <div class="xarxes">
                <div class="xarxes-top">
                    <h1>Ens trobaràs aqui:</h1>
                    <a href=""><span class="icon-phone"> +34 616616616</span></a>   
                </div>
                <div class="xarxes-bottom">
                    <div class="xarxes-1">
                        <a href=""><span class="icon-twitter-with-circle"></span><h2>Twitter</h2></a>
                        <a href=""><span class="icon-facebook"></span><h2>Facebook</h2></a>
                    </div>
                    <div class="xarxes-2">
                        <a href=""><span class="icon-youtube"></span><h2>Youtube</h2></a>
                        <a href=""><span class="icon-linkedin-with-circle"></span><h2>Linkedin</h2></a>
                    </div>
                </div>
            </div>
        </footer>
        <div class="credit">
            <h2>Viplay - 2022</h2>
        </div>
        </div>
    </form>
</body>
</html>
