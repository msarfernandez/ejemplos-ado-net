using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace winform_app
{
    public partial class frmPokemons : Form
    {
        private List<Pokemon> listaPokemon;
        public frmPokemons()
        {
            InitializeComponent();
        }

        private void frmPokemons_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);

        }

        private void cargar()
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                listaPokemon = negocio.listar();
                dgvPokemons.DataSource = listaPokemon;
                dgvPokemons.Columns["UrlImagen"].Visible = false;
                dgvPokemons.Columns["Id"].Visible = false;
                cargarImagen(listaPokemon[0].UrlImagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxPokemon.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxPokemon.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaPokemon alta = new frmAltaPokemon();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado;
            seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;

            frmAltaPokemon modificar = new frmAltaPokemon(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            Pokemon seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿De verdad querés eliminarlo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
