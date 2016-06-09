using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nini.Config;

namespace CameraCanvas
{
    public partial class Config : Form
    {
        string configFile;
        string default_folder = "/"; /* starting working directory */

        int defaultwidth = 300;
        int defaultheight = 300;

        bool showminmap = false;
        bool arrowInverted = false;

        bool saved = false;

        int movespeed = 1;

        int oldHeight = 0;
        int oldWidth = 0;

        IConfigSource config;

        public Config(string configFile)
        {
            InitializeComponent();
            this.configFile = configFile;
        }

        private void Config_Load(object sender, EventArgs e)
        {
            loadSettings();
            this.textBox1.Text = this.default_folder;
            this.textBox2.Text = this.defaultwidth.ToString();
            this.textBox3.Text = this.defaultheight.ToString();
            if (this.arrowInverted) this.checkBox1.Checked = true;
            this.label5.Text = this.movespeed.ToString();
            this.trackBar1.Value = this.movespeed;
        }

        private void loadSettings()
        {
            try
            {
                config = new IniConfigSource(configFile);

                if (config.Configs["Directories"].Contains("default"))
                {
                    default_folder = config.Configs["Directories"].GetString("default");
                }
                if (config.Configs["UI"].Contains("defaultwidth"))
                {
                    defaultwidth = config.Configs["UI"].GetInt("defaultwidth");
                    oldWidth = defaultwidth;
                }

                if (config.Configs["UI"].Contains("defaultheight"))
                {
                    defaultheight = config.Configs["UI"].GetInt("defaultheight");
                    oldHeight = defaultheight;
                }
                if (config.Configs["UI"].Contains("arrowInverted"))
                {
                    arrowInverted = config.Configs["UI"].GetBoolean("arrowInverted");
                }
                if (config.Configs["UI"].Contains("movespeed"))
                {
                    movespeed = config.Configs["UI"].GetInt("movespeed");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.ShowNewFolderButton = true;
            fd.Description = "Select a folder to load/save images";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = fd.SelectedPath + "\\";
            }
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.textBox2.Text = Int16.Parse(this.textBox2.Text).ToString();
            }
            catch
            {
                this.textBox2.Text = this.oldWidth.ToString();
            }
            this.oldWidth = Int16.Parse(this.textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.textBox3.Text = Int16.Parse(this.textBox3.Text).ToString();
            }
            catch
            {
                this.textBox3.Text = this.oldHeight.ToString();
            }
            this.oldHeight = Int16.Parse(this.textBox3.Text);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.label5.Text = this.trackBar1.Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            config.Configs["Directories"].Set("default", this.textBox1.Text); 
            config.Configs["UI"].Set("arrowInverted", this.checkBox1.Checked); 
            config.Configs["UI"].Set("defaultwidth", this.textBox2.Text); 
            config.Configs["UI"].Set("defaultheight", this.textBox3.Text);
            config.Configs["UI"].Set("movespeed", this.label5.Text);
            config.Save();
            this.Close();
          }
    }
}