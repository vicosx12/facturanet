using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facturanet;
using Facturanet.Entities;
using Facturanet.Server;
using Facturanet.Business;
using Facturanet.Infrastructure;

namespace Facturanet.WinformsClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CompositeRequest solicitudes = new CompositeRequest();


            SystemInfoRequest solicitud0 = new SystemInfoRequest();
            GetProductsRequest solicitud1 = new GetProductsRequest();

            solicitudes.Requests.Add(solicitud0);
            solicitudes.Requests.Add(solicitud1);

            CompositeResponse respuesta = this.checkBox1.Checked
                ? solicitudes.RunMock()
                : solicitudes.Run();


            SystemInfoResponse respuestaInfoSistema = respuesta.Responses[0] as SystemInfoResponse;
            Console.WriteLine("RESPUESTAINFOSISTEMA");
            Console.WriteLine(respuestaInfoSistema.ToString());


            GetProductsResponse respuestaRecuperarArticulos = respuesta.Responses[1] as GetProductsResponse;
            Console.WriteLine("*respuestaRecuperarArticulos*");
            Console.WriteLine(respuestaRecuperarArticulos.ToString());

            this.articuloBindingSource.DataSource = respuestaRecuperarArticulos.Products;
            //this.clienteBindingSource.DataSource = Servicios.BusinessServices.RecuperarClientes();
            //this.facturaBindingSource.DataSource = Servicios.BusinessServices.RecuperarFacturas();
            
            ActualizarItems();
        }

        private void facturaBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            ActualizarItems();
        }

        private void ActualizarItems()
        {
            //this.itemFacturaBindingSource.DataSource = facturaBindingSource.Current;
            //this.itemFacturaBindingSource.DataMember = "Items";
            /*
            Factura f = facturaBindingSource.Current as Factura;
            if (f == null || f.Id == 0)
            {
                this.itemFacturaBindingSource.DataSource = null;
            }
            else
            {
                Factura f2 = Servicios.BusinessServices.RecuperarFactura(f.Id);
                this.itemFacturaBindingSource.DataSource = f2.Items;
            }
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CompositeRequest requests = new CompositeRequest();
            requests.Requests.Add(
                new SystemInfoRequest()
            );
            requests.Requests.Add(
                new RemoveProductRequest()
                {
                    ProductIdentificator = new Selector<Product>() { Id = 2 }
                });
            requests.Requests.Add(
                new RemoveProductRequest()
                {
                    ProductIdentificator = new Selector<Product>()
                    {
                        Example = new Product() { Name = "Arr%" }
                    }
                });
            requests.Requests.Add(
                new RemoveProductRequest()
                {
                    ProductIdentificator = new Selector<Product>()
                    {
                        Example = new Product() { Name = "POLLO" }
                    }
                });
            /*
            requests.Requests.Add(
                new RemoveProductRequest()
                {
                    ProductIdentificator = new Selector<Product>()
                    {
                        Example = new Product() { Name = "%" } //devuelve varios
                    }
                });
            */
            try
            {
                CompositeResponse responses = requests.Run();
                foreach (Response r in responses.Responses)
                {
                    SystemInfoResponse sir = r as SystemInfoResponse;
                    RemoveProductResponse rpr = r as RemoveProductResponse;
                    if (rpr != null)
                        Console.WriteLine(rpr.RespuestaPrueba);
                    else if (sir != null)
                        Console.WriteLine(sir);
                    else
                        Console.WriteLine("Respuesta no esperada");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
