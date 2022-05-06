/*
 * Created by SharpDevelop.
 * User: Gustavo
 * Date: 16/03/2021
 * Time: 03:31 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Actividad04
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.imagen = new System.Windows.Forms.PictureBox();
			this.titulo = new System.Windows.Forms.Label();
			this.cargar = new System.Windows.Forms.Button();
			this.analisis = new System.Windows.Forms.Button();
			this.INFO = new System.Windows.Forms.Label();
			this.label_grafo = new System.Windows.Forms.Label();
			this.treeView_graph = new System.Windows.Forms.TreeView();
			this.label_NOIMG = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.imagen)).BeginInit();
			this.SuspendLayout();
			// 
			// imagen
			// 
			this.imagen.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.imagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.imagen.Location = new System.Drawing.Point(321, 56);
			this.imagen.Name = "imagen";
			this.imagen.Size = new System.Drawing.Size(396, 343);
			this.imagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.imagen.TabIndex = 0;
			this.imagen.TabStop = false;
			this.imagen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImagenMouseClick);
			// 
			// titulo
			// 
			this.titulo.Font = new System.Drawing.Font("Franklin Gothic Demi", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.titulo.Location = new System.Drawing.Point(29, 20);
			this.titulo.Name = "titulo";
			this.titulo.Size = new System.Drawing.Size(250, 52);
			this.titulo.TabIndex = 1;
			this.titulo.Text = "Actividad 04";
			// 
			// cargar
			// 
			this.cargar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.cargar.Font = new System.Drawing.Font("Franklin Gothic Demi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cargar.Location = new System.Drawing.Point(29, 86);
			this.cargar.Margin = new System.Windows.Forms.Padding(0);
			this.cargar.Name = "cargar";
			this.cargar.Size = new System.Drawing.Size(126, 67);
			this.cargar.TabIndex = 2;
			this.cargar.Text = "CARGAR IMAGEN";
			this.cargar.UseVisualStyleBackColor = false;
			this.cargar.Click += new System.EventHandler(this.CargarClick);
			// 
			// analisis
			// 
			this.analisis.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.analisis.Font = new System.Drawing.Font("Franklin Gothic Demi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.analisis.Location = new System.Drawing.Point(169, 86);
			this.analisis.Margin = new System.Windows.Forms.Padding(0);
			this.analisis.Name = "analisis";
			this.analisis.Size = new System.Drawing.Size(128, 64);
			this.analisis.TabIndex = 4;
			this.analisis.Text = "ANALIZAR IMAGEN";
			this.analisis.UseVisualStyleBackColor = false;
			this.analisis.Click += new System.EventHandler(this.AnalisisClick);
			// 
			// INFO
			// 
			this.INFO.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.INFO.ForeColor = System.Drawing.Color.Navy;
			this.INFO.Location = new System.Drawing.Point(29, 415);
			this.INFO.Name = "INFO";
			this.INFO.Size = new System.Drawing.Size(286, 99);
			this.INFO.TabIndex = 7;
			// 
			// label_grafo
			// 
			this.label_grafo.Font = new System.Drawing.Font("Franklin Gothic Book", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_grafo.Location = new System.Drawing.Point(29, 171);
			this.label_grafo.Name = "label_grafo";
			this.label_grafo.Size = new System.Drawing.Size(174, 23);
			this.label_grafo.TabIndex = 8;
			this.label_grafo.Text = "Info. Grafo";
			// 
			// treeView_graph
			// 
			this.treeView_graph.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.treeView_graph.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.treeView_graph.Location = new System.Drawing.Point(29, 209);
			this.treeView_graph.Name = "treeView_graph";
			this.treeView_graph.Size = new System.Drawing.Size(268, 190);
			this.treeView_graph.TabIndex = 9;
			// 
			// label_NOIMG
			// 
			this.label_NOIMG.Font = new System.Drawing.Font("Franklin Gothic Demi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_NOIMG.ForeColor = System.Drawing.Color.RoyalBlue;
			this.label_NOIMG.Location = new System.Drawing.Point(446, 186);
			this.label_NOIMG.Name = "label_NOIMG";
			this.label_NOIMG.Size = new System.Drawing.Size(148, 45);
			this.label_NOIMG.TabIndex = 10;
			this.label_NOIMG.Text = "NO IMAGE";
			this.label_NOIMG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1007, 690);
			this.Controls.Add(this.label_NOIMG);
			this.Controls.Add(this.treeView_graph);
			this.Controls.Add(this.label_grafo);
			this.Controls.Add(this.INFO);
			this.Controls.Add(this.analisis);
			this.Controls.Add(this.cargar);
			this.Controls.Add(this.titulo);
			this.Controls.Add(this.imagen);
			this.Name = "MainForm";
			this.Text = "Actividad04";
			((System.ComponentModel.ISupportInitialize)(this.imagen)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label_NOIMG;
		private System.Windows.Forms.TreeView treeView_graph;
		private System.Windows.Forms.Label label_grafo;
		private System.Windows.Forms.Label INFO;
		private System.Windows.Forms.Button analisis;
		private System.Windows.Forms.Button cargar;
		private System.Windows.Forms.Label titulo;
		private System.Windows.Forms.PictureBox imagen;
		

	}
}
