using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        BindingSource channelsbindingSource = new BindingSource();
        BindingSource videosBindingSource = new BindingSource();
        private List<Youtubers> videos;
        private WebBrowser webView21; // Adjusted declaration
        private object webBrowser21;

        public Form1()
        {
            InitializeComponent();
            webView21 = new WebBrowser(); // Adjusted instantiation
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChannelsDAO channelsDAO = new ChannelsDAO();

            channelsbindingSource.DataSource = channelsDAO.getAllYoutubers();
            dataGridView1.DataSource = channelsbindingSource;
            pictureBox1.Load("https://media.licdn.com/dms/image/C5603AQFbOqG9og1S5g/profile-displayphoto-shrink_800_800/0/1517251238442?e=1719446400&v=beta&t=MuapTwAlQ8nkSxwocrGIouU_lIu3SiN1yK51sUEF__w");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChannelsDAO channelsDAO = new ChannelsDAO();

            channelsbindingSource.DataSource = channelsDAO.searchTitles(textBox1.Text);
            dataGridView1.DataSource = channelsbindingSource;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ChannelsDAO channelsDAO = new ChannelsDAO();

            channelsbindingSource.DataSource = channelsDAO;
            dataGridView1.DataSource = channelsbindingSource;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            int rowclicked = dataGridView.CurrentRow.Index;
            String imageURL = dataGridView.Rows[rowclicked].Cells[4].Value.ToString();
            pictureBox1.Load(imageURL);
            ChannelsDAO channelsDAO = new ChannelsDAO();
            videosBindingSource.DataSource = channelsDAO.getVideo((int)dataGridView.Rows[rowclicked].Cells[0].Value);
            dataGridView2.DataSource = videosBindingSource;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Youtubers youtuber = new Youtubers
            {
                ChannelName = text_channel.Text,
                TotalsSubscribers = text_subs.Text,
                Speciality = text_spec.Text,
                ImageURL = text_image.Text,
                Description = text_description.Text
            };
            ChannelsDAO channelsDAO = new ChannelsDAO();
            int result = channelsDAO.addOneYoutuber(youtuber);
            MessageBox.Show(result + "New Row Inserted");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowclicked = dataGridView2.CurrentRow.Index;
            int videoID = (int)dataGridView2.Rows[rowclicked].Cells[0].Value;

            ChannelsDAO channelsDAO = new ChannelsDAO();
            int result = channelsDAO.deleteVideo(videoID);
            MessageBox.Show("Result" + result);
            dataGridView2.DataSource = null;
            videos = channelsDAO.getAllYoutubers();

        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            // Check if the clicked cell is not in the header row and a valid row index
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count)
            {
                // Get the index of the clicked row
                int rowClicked = e.RowIndex;

                // Check if the cell contains a value
                if (dataGridView.Rows[rowClicked].Cells[2].Value != null)
                {
                    String videoURL = dataGridView.Rows[rowClicked].Cells[2].Value.ToString();

                    // Load video URL in the WebBrowser control
                    webBrowser1.Navigate(videoURL);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Videos video = new Videos
            {
               Video_Title = text_channel.Text,
               Video_URL = text_subs.Text,
               youtubers_ID= Int32.Parse (text_yout.Text)

            };
            ChannelsDAO channelsDAO = new ChannelsDAO();
            int result = channelsDAO.addOneVideo(video);
            MessageBox.Show(result + " New Row Inserted");

        }
    }
}
    
