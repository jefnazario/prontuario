using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProntuarioAppAPI.Infra.Services
{
    /// <summary>
    /// Classe auxiliar com métodos para criptografia de dados.
    /// </summary>
    public class ServicoDeCriptografia
    {
        #region Private members
        private string key = string.Empty;
        private readonly CryptProvider cryptProvider;
        private readonly SymmetricAlgorithm algorithm;
        private void SetIV()
        {
            switch (cryptProvider)
            {
                case CryptProvider.Rijndael:
                    algorithm.IV = new byte[] { 0xf, 0x6f, 0xf9, 0x2e, 0x35, 0xc1, 0xcd, 0x13, 0x5, 0x26, 0x9c, 0xea, 0xa4, 0x7b, 0x73, 0xcc };
                    break;
                default:
                    algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0xc2, 0x35, 0x2e, 0xcd, 0xf4 };
                    break;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Chave secreta para o algoritmo simétrico de criptografia.
        /// </summary>
        public string Key => "LaniLibSeguranca";

        #endregion

        #region Constructors
        /// <summary>
        /// Contrutor padrão da classe, é setado um tipo de criptografia padrão.
        /// </summary>
        public ServicoDeCriptografia()
        {
            algorithm = new RijndaelManaged();
            algorithm.Mode = CipherMode.CBC;
            cryptProvider = CryptProvider.Rijndael;
        }
        /// <summary>
        /// Construtor com o tipo de criptografia a ser usada.
        /// </summary>
        /// <param name="cryptProvider">Tipo de criptografia.</param>
        private ServicoDeCriptografia(CryptProvider cryptProvider)
        {
            // Seleciona algoritmo simétrico
            switch (cryptProvider)
            {
                case CryptProvider.Rijndael:
                    algorithm = new RijndaelManaged();
                    this.cryptProvider = CryptProvider.Rijndael;
                    break;
                case CryptProvider.RC2:
                    algorithm = new RC2CryptoServiceProvider();
                    this.cryptProvider = CryptProvider.RC2;
                    break;
                case CryptProvider.DES:
                    algorithm = new DESCryptoServiceProvider();
                    this.cryptProvider = CryptProvider.DES;
                    break;
                case CryptProvider.TripleDES:
                    algorithm = new TripleDESCryptoServiceProvider();
                    this.cryptProvider = CryptProvider.TripleDES;
                    break;
            }
            algorithm.Mode = CipherMode.CBC;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gera a chave de criptografia válida dentro do array.
        /// </summary>
        /// <returns>Chave com array de bytes.</returns>
        private byte[] GetKey()
        {
            string salt = string.Empty;

            // Ajuta o tamanho da chave se necessário e retorna uma chave válida
            if (algorithm.LegalKeySizes.Length > 0)
            {
                // Tamanho das chaves em bits
                int keySize = this.key.Length * 8;
                int minSize = algorithm.LegalKeySizes[0].MinSize;
                int maxSize = algorithm.LegalKeySizes[0].MaxSize;
                int skipSize = algorithm.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    // Busca o valor máximo da chave
                    this.key = this.key.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    // Seta um tamanho válido
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                    {
                        // Preenche a chave com arterisco para corrigir o tamanho
                        this.key = this.key.PadRight(validSize / 8, '*');
                    }
                }
            }
            PasswordDeriveBytes key = new PasswordDeriveBytes(this.key, ASCIIEncoding.ASCII.GetBytes(salt));
            return key.GetBytes(this.key.Length);
        }
        /// <summary>
        /// Encripta o dado solicitado.
        /// </summary>
        /// <param name="plainText">Texto a ser criptografado.</param>
        /// <returns>Texto criptografado.</returns>
        public virtual string Criptografar(string plainText)
        {
            byte[] plainByte = ASCIIEncoding.ASCII.GetBytes(plainText);
            byte[] keyByte = GetKey();

            // Seta a chave privada
            algorithm.Key = keyByte;
            SetIV();

            // Interface de criptografia / Cria objeto de criptografia
            ICryptoTransform cryptoTransform = algorithm.CreateEncryptor();

            MemoryStream _memoryStream = new MemoryStream();

            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);

            // Grava os dados criptografados no MemoryStream
            _cryptoStream.Write(plainByte, 0, plainByte.Length);
            _cryptoStream.FlushFinalBlock();

            // Busca o tamanho dos bytes encriptados
            byte[] cryptoByte = _memoryStream.ToArray();

            // Converte para a base 64 string para uso posterior em um xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
        }
        /// <summary>
        /// Desencripta o dado solicitado.
        /// </summary>
        /// <param name="cryptoText">Texto a ser descriptografado.</param>
        /// <returns>Texto descriptografado.</returns>
        public virtual string Descriptografar(string cryptoText)
        {
            // Converte a base 64 string em num array de bytes
            byte[] cryptoByte = Convert.FromBase64String(cryptoText);
            byte[] keyByte = GetKey();

            // Seta a chave privada
            algorithm.Key = keyByte;
            SetIV();

            // Interface de criptografia / Cria objeto de descriptografia
            ICryptoTransform cryptoTransform = algorithm.CreateDecryptor();
            try
            {
                MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);

                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);

                // Busca resultado do CryptoStream
                StreamReader _streamReader = new StreamReader(_cryptoStream);
                return _streamReader.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }

    /// <summary>
    /// Enumerator com os tipos de classes para criptografia.
    /// </summary>
    public enum CryptProvider
    {
        /// <summary>
        /// Representa a classe base para implementações criptografia dos algoritmos simétricos Rijndael.
        /// </summary>
        Rijndael,
        /// <summary>
        /// Representa a classe base para implementações do algoritmo RC2.
        /// </summary>
        RC2,
        /// <summary>
        /// Representa a classe base para criptografia de dados padrões (DES - Data Encryption Standard).
        /// </summary>
        DES,
        /// <summary>
        /// Representa a classe base (TripleDES - Triple Data Encryption Standard).
        /// </summary>
        TripleDES
    }
}