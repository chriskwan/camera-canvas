using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nini.Config;


namespace WindowsApplication1
{
    public partial class Form2 : Form
    {
       public Form2()
        {
            

        }

        private void new_settings()
        {
            config.Set("config.ini", "MyNewSavedFile.log");
            IConfigSource source = new IniConfigSource("config.ini");
                        
            int newwidth = System.Convert.ToInt32(this.textBox1.Text);
            int newheight = System.Convert.ToInt32(this.textBox2.Text);
            int newscrollspeed = System.Convert.ToInt32(this.textBox3.Text);
            int newtoolbarspeed = System.Convert.ToInt32(this.textBox4.Text);
            int newshowminmap = System.Convert.ToInt32(this.textBox5.Text);
            int newarrowInverted = System.Convert.ToInt32(this.textBox6.Text);

            config.Set(defaultwidth, newwidth);
            config.Set(defaultheight, newheight);
            config.Set(scrollspeed, newscrollspeed);
            config.Set(toolbarspeed, newtoolbarspeed);
            config.Set(showminmap, newshowminmap);
            config.Set(arrowInverted, newarrowInverted);

            source.Save();
            

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int width = source.Configs["UI"].GetInt("defaultwidth");
            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}