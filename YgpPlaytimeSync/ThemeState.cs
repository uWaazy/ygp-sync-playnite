using System.ComponentModel;

namespace YgpPlaytimeSync
{
    /// <summary>
    /// Estado do jogo selecionado, exposto para temas do Playnite (ex.: Dune YGP).
    /// O tema lê com {PluginSettings Plugin=YgpSync, Path=HasData} (e demais propriedades).
    /// </summary>
    public class ThemeState : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _hasData;
        private bool _ignored;
        private bool _synced;
        private string _hours = "";
        private string _account = "";

        /// <summary>true = conta vinculada e há um jogo único selecionado.</summary>
        public bool HasData { get { return _hasData; } private set { if (_hasData != value) { _hasData = value; Raise("HasData"); } } }

        /// <summary>true = este jogo está marcado para não sincronizar.</summary>
        public bool Ignored { get { return _ignored; } private set { if (_ignored != value) { _ignored = value; Raise("Ignored"); } } }

        /// <summary>true = já existe tempo deste jogo enviado ao Your Gamer Profile.</summary>
        public bool Synced { get { return _synced; } private set { if (_synced != value) { _synced = value; Raise("Synced"); } } }

        /// <summary>Tempo já enviado, formatado (ex.: 12h 30m). Vazio se nada foi enviado.</summary>
        public string Hours { get { return _hours; } private set { if (_hours != value) { _hours = value ?? ""; Raise("Hours"); } } }

        /// <summary>Nome de usuário da conta vinculada.</summary>
        public string Account { get { return _account; } private set { if (_account != value) { _account = value ?? ""; Raise("Account"); } } }

        public void Update(bool connected, bool ignored, bool synced, string hours, string account)
        {
            Account = account;
            Hours = hours;
            Synced = synced;
            Ignored = ignored;
            HasData = connected;
        }

        public void Clear() { Update(false, false, false, "", ""); }

        private void Raise(string name)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
