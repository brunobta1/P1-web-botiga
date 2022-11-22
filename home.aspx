<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Projecte_1.WebForm1" %>

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
<body>
    <form id="form1" runat="server">
        

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
            <a href="carret.aspx" class="icono_carret"><span class="icon-shopping-cart"><label class="num_carrito" id="totalproductes" runat="server">0 Productes</label></span></a>
        </header>


        <div class="portada">
            <div class="contingut-portada">
                <h1>Juga sempre al màxim</h1>
                <p>Uneix-te a la comunitat gaming i juga al màxim nivell</p>
                <a href="#stop">Productes</a>
            </div>
        </div>

        <div class="info">
            <div id="info-1">
                <img id="img1" src="imatges/info1.png" />
                <p>A la nostra botiga trobaràs tot tipus de perifèrics i elements gaming adaptats als usuaris amb qualsevol pressupost per a millorar la teva experiència de joc.</p>
            </div>
            <div id="info-2">
                <p>Enviem a tot el món i la nostra prioritat és que el teu temps d'espera per rebre l'equìp sigui el menor possible. No perdis ni un segon de més!</p>
                <div id="illustracio">
                    <img id="img2" src="imatges/info2.png" />
                    <p id="stop"></p>
                </div>
            </div>
        </div>

        <div id="principal">
            <h1>Els nostres productes</h1>

            <div id="productes" runat="server">
                
                
                


            </div>

            <div id="buit" class="no-mostrar" runat="server">
                <img id="no-result" src="imatges/noresult.png" />
                <h1>Ara no hi tenim productes a la venda, torni aviat!</h1>
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

    </form>
</body>
</html>
