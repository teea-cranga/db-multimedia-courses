<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImaginiGandaci.aspx.cs" Inherits="Seminar1_BDMultimedia.ImaginiGandaci" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:Label ID="BugApp" runat="server" BackColor="#FF66CC" Text="Bug app idk"></asp:Label>
        </p>
        <p>
            ID imagine:
            <asp:TextBox ID="TextBoxID" runat="server" Width="101px"></asp:TextBox>
            <br />
            Denumire gandac:
            <asp:TextBox ID="TextBoxDen" runat="server"></asp:TextBox>
            <br />
            Denumire stiintifica gandac:
            <asp:TextBox ID="TextBoxDenSt" runat="server"></asp:TextBox>
            <br />
            Imagine: <asp:FileUpload ID="FileUpload1" runat="server" />
        </p>
        <p>
            <asp:Button ID="ButtonUpload" runat="server" OnClick="ButtonUpload_Click" Text="Insereaza imaginea" />
        </p>
        <p>
            <asp:Label runat="server" Text="ID imagine:" ID="id_img"></asp:Label><asp:TextBox runat="server" ID="tb_afis"></asp:TextBox>&nbsp;<asp:Button runat="server" Text="Afiseaza imaginea" OnClick="afiseaza_imagine_Click" ID="afiseaza_imagine"></asp:Button>
        </p>
            <asp:Image runat="server" BorderStyle="Groove" ID="afis_img"></asp:Image>
            <asp:Button runat="server" Text="Generare semnaturi" ID="gen_semn" OnClick="gen_semn_Click"></asp:Button>
        <p>&nbsp;</p>
        <p>
            <asp:Label runat="server" Text="Coeficient importanta culoare:" ID="label_c"></asp:Label><asp:TextBox runat="server"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Coeficient importanta textura:" ID="label_t"></asp:Label><asp:TextBox runat="server"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Coeficient importanta forma:" ID="label_f"></asp:Label><asp:TextBox runat="server"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Coeficient importanta locatie:" ID="label_l"></asp:Label><asp:TextBox runat="server"></asp:TextBox>
            <br />
            <asp:Label runat="server" Text="Fisierul de cautat este: " ID="label_ct"></asp:Label><asp:FileUpload ID="FileUploadC" runat="server" />
            <asp:Button runat="server" Text="Cauta fisier"/>
        </p>
    </form>
</body>
</html>
