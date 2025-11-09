<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImaginiGandaci.aspx.cs" Inherits="Seminar1_BDMultimedia.ImaginiGandaci" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>
            <asp:Label ID="BugApp" runat="server" BackColor="#993300" Text="BugID"></asp:Label>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Adauga video unei postari" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Vezi toate postarile" />
        </h1>
        <p>
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
            <asp:Label runat="server" Text="ID imagine:" ID="id_img"></asp:Label><asp:TextBox runat="server" ID="tb_afis"></asp:TextBox>
        </p>
        <p>
            &nbsp;<asp:Button runat="server" Text="Afiseaza imaginea" OnClick="afiseaza_imagine_Click" ID="afiseaza_imagine" style="margin-bottom: 0px"></asp:Button>
        </p>
            <asp:Image runat="server" BorderStyle="Groove" ID="afis_img" Height="124px" Width="214px"></asp:Image>
        <p>
            <asp:Button runat="server" Text="Generare semnaturi" ID="gen_semn" OnClick="gen_semn_Click"></asp:Button>
        </p>
        <p>
            <asp:Label runat="server" Text="Fisierul de cautat este: " ID="label_ct"></asp:Label><asp:FileUpload ID="FileUploadS" runat="server" />
        </p>
        <p>
            <asp:Label ID="LabelDescriere" runat="server" Text=".."></asp:Label>
        </p>
        <p>
            <asp:Button runat="server" Text="Cauta fisier" OnClick="Unnamed5_Click"/>
        </p>
    </form>
</body>
</html>
