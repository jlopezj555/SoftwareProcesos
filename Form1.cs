using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessKiller
{ 
    public partial class Form1 : Form 
{
    public Form1() 
    { 
        InitializeComponent();
        UpdateProcessList();
        timer1.Enabled = true;
    }
    private void UpdateProcessList() {
        lstProcesses.Items.Clear();
        lst_id.Items.Clear();
        lst_memoriafisica.Items.Clear();
        lst_memoriavirtual.Items.Clear();
        lst_CPU.Items.Clear();
        int id = 1;
        foreach (Process p in Process.GetProcesses()) 
        {
            lstProcesses.Items.Add(id + ":" + p.ProcessName); // nombre del proceso
            lst_id.Items.Add(id + ": " + p.Id);// id del proceso
            lst_memoriafisica.Items.Add(id + ": " + p.WorkingSet64);// RAM del proceso
            lst_memoriavirtual.Items.Add(id + ": " + p.VirtualMemorySize64); // MEmoria virtual del proceso
            lst_CPU.Items.Add(id + ": " + p.SessionId); // CPU que usa el proceso
           
            id = id + 1;     
        }
        tslProcessCount.Text = "Procesos Actuales: " + lstProcesses.Items.Count.ToString();    //  cant de procesos   
    } 
        
private void btnKill_Click(object sender, EventArgs e) {

    try
    {
        foreach (Process p in Process.GetProcesses())
        {
            string arr = lstProcesses.SelectedItem.ToString(); // proceso seleccionado en el list box
            string[] proceso = arr.Split(':');// divido el contenido del listbox
 

            if (p.ProcessName == proceso[1])
            {
                p.Kill(); // elimina el proceso
            }
           
        }
    }
    catch (Exception x){
        MessageBox.Show("no selecciono nigun proceso " + x,"error al eliminar",MessageBoxButtons.OK);
    }
   
    //UpdateProcessList(); 
} 

        
        private void button1_Click(object sender, EventArgs e) { 
            Close(); 
        } 
        private void button2_Click(object sender, EventArgs e) {
            string procesoABuscar = txtBoxBuscar.Text.Trim(); // Obtener el nombre del proceso ingresado por el usuario
            if (!string.IsNullOrEmpty(procesoABuscar))
            {
                int indiceProcesoEncontrado = -1; // Inicializamos en -1 para indicar que no se ha encontrado
                for (int i = 0; i < lstProcesses.Items.Count; i++)
                {
                    string procesoEnLista = lstProcesses.Items[i].ToString();
                    if (procesoEnLista.IndexOf(procesoABuscar, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        // Mostrar información del proceso encontrado
                        string nombreProcesoEncontrado = procesoEnLista.Split(':')[1].Trim(); // Obtener el nombre del proceso encontrado
                        MessageBox.Show($"Proceso encontrado: {nombreProcesoEncontrado}\nID: {lst_id.Items[i].ToString().Split(':')[1].Trim()}", "Proceso encontrado", MessageBoxButtons.OK);
                        indiceProcesoEncontrado = i; // Guardar la posición del proceso encontrado
                        break; // Salir del bucle al encontrar el primer proceso con el nombre dado
                    }
                }

                if (indiceProcesoEncontrado != -1)
                {
                    lstProcesses.SelectedIndex = indiceProcesoEncontrado;
                }
                else
                {
                    // Si no se encontró el proceso, mostrar un mensaje
                    MessageBox.Show($"No se encontró ningún proceso que contenga el nombre '{procesoABuscar}'.", "Proceso no encontrado", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un nombre de proceso válido.", "Entrada inválida", MessageBoxButtons.OK);
            }
        }

        private void btnUpdateProcessList_Click_1(object sender, EventArgs e)
        {
         UpdateProcessList(); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateProcessList(); 
        } 
    }
}
