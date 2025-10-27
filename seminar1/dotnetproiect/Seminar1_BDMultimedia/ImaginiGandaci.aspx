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
    </form>
</body>
</html>
