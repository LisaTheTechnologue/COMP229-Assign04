<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Model.aspx.cs" Inherits="COMP229_Assign04.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--a.	display data about the individual model selected in Instruction 3(d). 
b.	list the model’s data and display the image in its link.
c.	include an Update link to the Update page, or attain equivalent functionality in page. 
d.	select all necessary model data via LINQ queries. 
e.	include a delete button to remove a model (and redirect to the home page). 
-->
    <asp:DetailsView ID="modelDetail" runat="server" AutoGenerateRows="false" >
        <Fields>
            <asp:BoundField DataField="charName" HeaderText="Name" />
            <asp:BoundField DataField="faction" HeaderText="Faction" />
            <asp:BoundField DataField="rank" HeaderText="Rank" />
            <asp:BoundField DataField="_base" HeaderText="Base" />
            <asp:BoundField DataField="size" HeaderText="Size" />
            <asp:BoundField DataField="deploymentZone" HeaderText="Deployment Zone" />
            <asp:BoundField DataField="traits" HeaderText="Traits" />
            <asp:BoundField DataField="types" HeaderText="Types" />
            <asp:BoundField DataField="defenseChart" HeaderText="Defense Chart" />
            <asp:BoundField DataField="mobility" HeaderText="Mobility" />
            <asp:BoundField DataField="willpower" HeaderText="Will Power" />
            <asp:BoundField DataField="resiliance" HeaderText="Resiliance" />
            <asp:BoundField DataField="wounds" HeaderText="Wounds" />
            <asp:ImageField DataImageUrlField="imageUrl" DataAlternateTextField="imageUrl" DataAlternateTextFormatString="imageUrl" HeaderText="Image" />
        </Fields>

    </asp:DetailsView>
    <asp:DetailsView ID="actionDetail" runat="server" AutoGenerateRows="false">
        <Fields>
            <asp:BoundField DataField="name" HeaderText="Actions" />
            <asp:BoundField DataField="type" HeaderText="Type" />
            <asp:BoundField DataField="rating" HeaderText="Rating" />
            <asp:BoundField DataField="range" HeaderText="Range" />
        </Fields>
    </asp:DetailsView>
    <asp:DetailsView ID="SADetail" runat="server" AutoGenerateRows="false">
        <Fields>
            <asp:BoundField DataField="name" HeaderText="Special Abilities" />
            <asp:BoundField DataField="description" HeaderText="Description" />
            
        </Fields>
    </asp:DetailsView>
    <asp:Button ID="updateBtn" runat="server" Text="Update" OnClick="updateBtn_Click" />
    <asp:Button ID="deleteBtn" runat="server" Text="Delete" />

</asp:Content>
