/*
 * Created by SharpDevelop.
 * User: Moloz
 * Date: 6/12/2017
 * Time: 9:38 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using Microsoft.VisualBasic;

namespace taskmgr
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		//afisare procese active
		void Button1Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
			GetProcesses();
			
		}
		//buton care afiseaza procesele active in listbox
		void GetProcesses(){
			Process[] procese=Process.GetProcesses();
			foreach(Process proc in procese){
				listBox1.Items.Add(proc.ProcessName);
			}
		
		}
		//buton care incheie executia unui proces
		void Button2Click(object sender, EventArgs e)
		{
			if(listBox1.SelectedItem==null)
				MessageBox.Show("Alegeti un proces!","Warning");
			else
			killProcess();
			
		}
		//functie care incheie executia unui proces selectat
		void killProcess(){
		Process[] procese=Process.GetProcesses();
		foreach(Process proc in procese){
			if(listBox1.SelectedItem.ToString()==proc.ProcessName){
				proc.Kill();
			MessageBox.Show("Proces inchis cu succes!","Succes");
			break;
			
			}
			
			
		
		}
		
		
		}
		
	//
		
		
	//functie care preia utilizatorul procesului selectat	
	public string GetProcessOwner(int processId)
{
    string query = "Select * From Win32_Process Where ProcessID = " + processId;
    ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
    ManagementObjectCollection processList = searcher.Get();

    foreach (ManagementObject obj in processList)
    {
        string[] argList = new string[] { string.Empty, string.Empty };
        int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
        if (returnVal == 0)
        {
            
            return argList[1] + "\\" + argList[0];
        }
    }

    return "user inexistent";
}
	
		
		//buton care afiseaza proprietatile unui proces ales
		void Button3Click(object sender, EventArgs e)
		{
			if(listBox1.SelectedItem!=null){
			try{
			Process[] procese=Process.GetProcesses();
		foreach(Process proc in procese){
				if(listBox1.SelectedItem.ToString()==proc.ProcessName){
					
					string temp=string.Empty;
					temp+="ID proces : "+proc.Id.ToString();
					temp+="\nLocatie : "+proc.MainModule.FileName.ToString();
					temp+="\nUser : "+GetProcessOwner(proc.Id);
					MessageBox.Show(temp,"Proprietati "+proc.ProcessName);
					break;
					
					
				
				}
			
			}
			}catch(Exception ){
						MessageBox.Show("Acces interzis!","Eroare");
					}
			
			
			}
			else
				MessageBox.Show("Alegeti un proces!","Warning");
		}

		//buton care permite rularea unui program la alegere
		void Button4Click(object sender, EventArgs e)
		{
			string cale=Microsoft.VisualBasic.Interaction.InputBox("Cale catre proces  (Implicit C:/Windows/System32)","Executa","",350,350);
			try{
				Process.Start(cale);}
			catch(Exception){
			
			}
			
		}
	}
}
