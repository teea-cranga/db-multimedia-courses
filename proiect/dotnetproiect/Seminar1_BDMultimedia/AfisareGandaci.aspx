<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AfisareGandaci.aspx.cs" Inherits="Seminar1_BDMultimedia.AfisareGandaci" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <h1>
            <asp:Label ID="showPicsLbl" runat="server" BackColor="#993300" Text="BugID - Toate postarile"></asp:Label>
            <asp:Button ID="Button_img" runat="server" Text="Postare noua" OnClick="ImaginiGandaci_Click" />
            <asp:Button ID="Button_vid" runat="server" Text="Adauga video" OnClick="AdaugaVideo_Click" />
        </h1>
        <p>
            <asp:Image ID="Image1" runat="server" />
        </p>
        <p>
            &nbsp;</p>

    </form>
</body>
</html>
