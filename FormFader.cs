using System.ComponentModel;

namespace ControlX
{
    public class FormFader : Component
    {
        #region Declarações     --------------------------------------------------------------------------------------------------------------------------------------------
        Form _parent;
        public enum EnumFadeSpeed
        {
            Slow = 1, Medium = 2, Fast = 3
        }
        #endregion Declarações  --------------------------------------------------------------------------------------------------------------------------------------------

        #region Construtores    --------------------------------------------------------------------------------------------------------------------------------------------
        public FormFader() { }

        #endregion Construtores --------------------------------------------------------------------------------------------------------------------------------------------

        #region Eventos         --------------------------------------------------------------------------------------------------------------------------------------------
        private void Form_Shown(object sender, System.EventArgs e) { FadeIn(); }
        private void Form_FormClosing(object sender, FormClosingEventArgs e) { FadeOut(); }

        #endregion Eventos      --------------------------------------------------------------------------------------------------------------------------------------------

        #region Funções         --------------------------------------------------------------------------------------------------------------------------------------------
        private void FadeIn()
        {
            if (Canfade() == false) return;

            _parent.Opacity = 0;
            _parent.Refresh();
            _parent.BringToFront();
            _parent.Visible = true;

            for (double i = 0; i < 1; i += 0.020)
            {
                Thread.Sleep(1);
                _parent.Opacity = i;
            }
        }
        private void FadeOut()
        {
            if (Canfade() == false) return;

            while (_parent.Opacity > 0)
            {
                Thread.Sleep(1);
                _parent.Opacity -= ((int)FadeSpeed * 0.020);
            }
        }
        private bool Canfade()
        {
            if (CancelFade) return false;

            if (_parent == null)
            {
                MessageBox.Show("A propriedade Parent não foi definida!");
                return false;
            }

            if (FadeSpeed == 0)
            {
                MessageBox.Show("A propriedade FadeSpeed não foi definida!");
                return false;
            }

            return true;
        }
        #endregion Funções      --------------------------------------------------------------------------------------------------------------------------------------------

        #region Propriedades    --------------------------------------------------------------------------------------------------------------------------------------------

        [System.ComponentModel.Category("Behavior")]
        [System.ComponentModel.DefaultValue(false)]
        public bool CancelFade { get; set; } = false;


        [System.ComponentModel.Category("Behavior")]
        [System.ComponentModel.DefaultValue(EnumFadeSpeed.Medium)]
        public EnumFadeSpeed FadeSpeed { get; set; } = EnumFadeSpeed.Medium;


        [System.ComponentModel.Category("Behavior")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Form Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;

                if (_parent != null)
                {
                    _parent.FormClosing += Form_FormClosing;
                    _parent.Shown += Form_Shown;
                }
            }
        }

        #endregion Propriedades --------------------------------------------------------------------------------------------------------------------------------------------
    }
}