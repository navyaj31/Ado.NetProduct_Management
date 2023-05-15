using Spectre.Console;
using System.Data;
using System.Data.SqlClient;
namespace ProductManagement
{
    public class Product_Management
    {
        SqlConnection con = new SqlConnection("server=IN-8HRQ8S3; database=ProductManagement; Integrated Security = True");

        public void Add_Product()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Product", con);
            adp.Fill(ds);
            string Name = AnsiConsole.Ask<string>("[purple]Enter Name of the product[/]");
            string Description = AnsiConsole.Ask<string>("[purple]Enter description of the product[/]");
            int Quantity = AnsiConsole.Ask<int>("[purple]Enter the Quantity[/]");
            int Price = AnsiConsole.Ask<int>("[purple]Enter the price of product[/]");
            var row = ds.Tables[0].NewRow();
            row["Product_Name"] = Name;
            row["Product_Description"] = Description;
            row["Quantity"] = Quantity;
            row["Price"] = Price;
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder1 = new SqlCommandBuilder(adp);
            adp.Update(ds);
            AnsiConsole.Write(new Markup("[deepskyblue2]Product is successfully added[/]"));
            
        }
        public void View_Product()
        {
            DataSet ds = new DataSet();
            int Id = AnsiConsole.Ask<int>("[aquamarine1]Enter Id: [/]");
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from  product where Product_Id ={Id}", con);
            adp.Fill(ds);

            Table table = new Table();
            table.AddColumn("Product_Id");
            table.AddColumn("Product_Name");
            table.AddColumn("Product_Description");
            table.AddColumn("Quantity");
            table.AddColumn("Price");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString());
            }
            AnsiConsole.Write(table);
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }

        public void View_AllProducts()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Product", con);
            adp.Fill(ds);
            Table table = new Table();
            table.AddColumn("Product_Id");
            table.AddColumn("Product_Name");
            table.AddColumn("Product_Description");
            table.AddColumn("Quantity");
            table.AddColumn("Price");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString());

            }
            AnsiConsole.Write(table);
            Console.WriteLine("-----------------------------------------------------------------------------------");
        }

        public void Update_Product()
        {
            DataSet ds = new DataSet();
            int Id = AnsiConsole.Ask<int>("[aquamarine1]Enter Id: [/]");
            SqlDataAdapter adp = new SqlDataAdapter($"Select *from product where Product_Id = {Id}", con);
            adp.Fill(ds);
            string Name = AnsiConsole.Ask<string>("[indianred]Enter Title of the product[/]");
            string Description = AnsiConsole.Ask<string>("[indianred]Enter description of the product[/]");
            int Quantity = AnsiConsole.Ask<int>("[indianred]Enter the Quantity[/]");
            int Price = AnsiConsole.Ask<int>("[indianred]Enter the price of product[/]");
            var row = ds.Tables[0].Rows[0];
            row["Product_Name"] = Name;
            row["Product_Description"] = Description;
            row["Quantity"] = Quantity;
            row["Price"] = Price;
            
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            AnsiConsole.Write(new Markup("[darkolivegreen3]Product is successfully Updated[/]"));
            
        }
        public void Delete_Product()
        {
            DataSet ds = new DataSet();
            int Id = AnsiConsole.Ask<int>("[lightsteelblue]Enter Id: [/]");
            SqlDataAdapter adp = new SqlDataAdapter($"Select *from product where Product_Id = {Id}", con);
            adp.Fill(ds);
            ds.Tables[0].Rows[0].Delete();
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            AnsiConsole.Write(new Markup("[tan] product is Successfully Deleted[/]"));

            
        }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(new FigletText("Welcome to Product Management").Centered().Color(Color.PaleVioletRed1));
            Product_Management product_management = new Product_Management();
            while (true)
            {
                Console.WriteLine();
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[hotpink3_1] Select your choice : [/]")
                    .AddChoices(new[]
                    {
                        "Add_Product",
                        "View_Product",
                        "View_AllProducts",
                        "Update_Product",
                        "Delete_Product"
                    }));
                switch (choice)
                {
                    case "Add_Product":
                        {
                            product_management.Add_Product();
                            break;
                        }
                    case "View_Product":
                        {
                            product_management.View_Product();
                            break;
                        }
                    case "View_AllProducts":
                        {
                            product_management.View_AllProducts();
                            break;
                        }
                    case "Update_Product":
                        {
                            product_management.Update_Product();
                            break;
                        }
                    case "Delete_Product":
                        {
                            product_management.Delete_Product();
                            break;
                        }
                    default:
                        {
                            AnsiConsole.Write("[plum2]Invalid Choice [/]");
                            break;

                        }

                }


            }
        }
    }
}

