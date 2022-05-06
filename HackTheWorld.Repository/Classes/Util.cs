using System;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using Api.Repository.Oracle.classes;

namespace Api.Repository.Classes
{
    public class Util
    {

        public static string ArquivoRetornarParteDeUmaString(string conteudo, string inicial, string final)
        {
            string retorno = string.Empty;

            if (conteudo.IndexOf(inicial) > -1)
            {
                retorno = conteudo.Substring(conteudo.IndexOf(inicial), (conteudo.IndexOf(final) + final.Length) - conteudo.IndexOf(inicial));
            }

            return retorno;
        }

        public static bool ValidarDatas(string Data)
        {
            bool retorno = true;

            DateTime dataValida;

            if (!DateTime.TryParse(Data, out dataValida))
            {
                retorno = false;
            }

            return retorno;
        }

        public static bool ValidarNumeros(string pValor)
        {
            bool retorno = true;

            double valorValido;

            if (!Double.TryParse(pValor, out valorValido))
            {
                retorno = false;
            }

            return retorno;
        }

        public static string TrocarSeparadorHora(string pHora)
        {
            try
            {

                return pHora.Substring(0, 2) + ":" + pHora.Substring(2, 2) + ":" + pHora.Substring(4, 2);
            }
            catch (Exception ex)
            {
                return pHora;
            }
        }

        public static string FormatarDataHora(string pData, TipoDeData pTipoDoRetorno = TipoDeData.DataHora_DDMMYYYYHHMMSS, bool pMostrarSeparadores = true, string pSeparadorData = "/", string pSeparadorHora = ":", string pSeparadorEntreData_E_Hora = " ", bool pFormatarPorExtenco = false, bool pExibirFeira = false)
        {
            string retorno = string.Empty;

            if (ValidarDatas(pData) == true)
            {
                DateTime agora = DateTime.Parse(pData);

                switch (pTipoDoRetorno)
                {
                    case TipoDeData.DataHora_DDMMYYYYHHMMSS:
                        retorno = agora.ToString("dd/MM/yyyy HH:mm:ss");
                        break;

                    case TipoDeData.DataHora_DDMMYYYYHHMM:
                        retorno = agora.ToString("dd/MM/yyyy HH:mm");
                        break;

                    case TipoDeData.DataHora_DDMMYYHHMMSS:
                        retorno = agora.ToString("dd/MM/yy HH:mm:ss");
                        break;

                    case TipoDeData.DataHora_DDMMYYHHMM:
                        retorno = agora.ToString("dd/MM/yy HH:mm");
                        break;

                    case TipoDeData.DataHora_YYYYMMDDHHMMSS:
                        retorno = agora.ToString("yyyy/MM/dd HH:mm:ss");
                        break;

                    case TipoDeData.DataHora_YYYYMMDDHHMM:
                        retorno = agora.ToString("yyyy/MM/dd HH:mm");
                        break;

                    case TipoDeData.DataHora_YYYYMMDD:
                        retorno = agora.ToString("yyyy/MM/dd");
                        break;

                    case TipoDeData.Data_DDMMYYYY:
                        retorno = agora.ToString("dd/MM/yyyy");
                        break;

                    case TipoDeData.Data_DDMMYY:
                        retorno = agora.ToString("dd/MM/yy");
                        break;

                    case TipoDeData.Data_YYYY:
                        retorno = agora.ToString("yyyy");
                        break;

                    case TipoDeData.Data_YYMM:
                        retorno = agora.ToString("yy/MM");
                        break;

                    case TipoDeData.Data_YYYYMM:
                        retorno = agora.ToString("yyyy/MM");
                        break;

                    case TipoDeData.Hora_HHMMSS:
                        retorno = agora.ToString("HH:mm:ss");
                        break;

                    case TipoDeData.Hora_HHMM:
                        retorno = agora.ToString("HH:mm");
                        break;

                    default:
                        retorno = agora.ToString("dd/MM/yyyy HH:mm:ss");
                        break;

                }


                if (pMostrarSeparadores == true)
                {
                    if (pSeparadorData != "/")
                    {
                        retorno = retorno.Replace("/", pSeparadorData);
                    }

                    if (pSeparadorHora != ":")
                    {
                        retorno = retorno.Replace(":", pSeparadorHora);
                    }

                    if (pSeparadorEntreData_E_Hora != " ")
                    {
                        retorno = retorno.Replace(" ", pSeparadorEntreData_E_Hora);
                    }
                }
                else
                {
                    retorno = retorno.Replace("/", "").Replace(":", "").Replace(" ", "");
                }

            }


            //17/04/2015 - Metodo para retornar dia da Semana, o correto é mudar a cultura da aplicação na inicialização para pt-BR
            if (pFormatarPorExtenco)
            {
                string vAuxDia = "Domingo";
                string vDia = "Sunday";//Domingo
                string vAuxMes = "Janeiro";
                int vMes = 1;       //Janeiro
                int vNumDia = 1;
                int vAno = 1990;

                if (ValidarDatas(retorno))
                {
                    DateTime vData = DateTime.Parse(retorno);
                    vDia = vData.DayOfWeek.ToString();
                    vNumDia = vData.Day;
                    vMes = vData.Month;
                    vAno = vData.Year;

                    if (vDia.ToLower() == "monday")
                        vAuxDia = "Segunda";
                    else if (vDia.ToLower() == "tuesday")
                        vAuxDia = "Terça";
                    else if (vDia.ToLower() == "wednesday")
                        vAuxDia = "Quarta";
                    else if (vDia.ToLower() == "thursday")
                        vAuxDia = "Quinta";
                    else if (vDia.ToLower() == "friday")
                        vAuxDia = "Sexta";
                    else if (vDia.ToLower() == "saturday")
                        vAuxDia = "Sábado";
                    else
                        vAuxDia = "Domingo";

                    if (pExibirFeira)
                        if (vDia.ToLower() != "sunday" && vDia.ToLower() != "saturday")
                            vAuxDia += "-Feira";
                        else
                            retorno = "Data informada não é valida.";

                    switch (vMes)
                    {
                        case 2:
                            vAuxMes = "fevereiro";
                            break;
                        case 3:
                            vAuxMes = "março";
                            break;
                        case 4:
                            vAuxMes = "abril";
                            break;
                        case 5:
                            vAuxMes = "maio";
                            break;
                        case 6:
                            vAuxMes = "junho";
                            break;
                        case 7:
                            vAuxMes = "julho";
                            break;
                        case 8:
                            vAuxMes = "agosto";
                            break;
                        case 9:
                            vAuxMes = "setembro";
                            break;
                        case 10:
                            vAuxMes = "outubro";
                            break;
                        case 11:
                            vAuxMes = "novembro";
                            break;
                        case 12:
                            vAuxMes = "dezembro";
                            break;
                    }
                }

                retorno = vAuxDia + ", " + vNumDia + " de " + vAuxMes.ToLower() + " de " + vAno;
            }

            return retorno;
        }

        public static bool DiretorioExiste(string pDiretorio)
        {
            bool retorno = true;

            if (Directory.Exists(pDiretorio) == false)
            {
                try
                {
                    Directory.CreateDirectory(pDiretorio);
                }
                catch (Exception ex)
                {

                    retorno = false;
                }

            }

            return retorno;

        }

        public static bool DiretorioCriar(string pDiretorio)
        {
            bool retorno = true;

            if (Directory.Exists(pDiretorio) == false)
            {
                try
                {
                    Directory.CreateDirectory(pDiretorio);
                }
                catch (Exception ex)
                {

                    retorno = false;
                }

            }

            return retorno;

        }

        public static string ArquivoRetornarConteudo(string pCaminhoDoArquivo, TipoDaCodificaoDoArquivo pTipoDaCodificacao = TipoDaCodificaoDoArquivo.UTF8)
        {
            string retorno = string.Empty;

            if (!File.Exists(pCaminhoDoArquivo))
            {
                retorno = "Arquivo não existe! \n\r" + pCaminhoDoArquivo;
            }
            else
            {

                //// Abre o arquivo utilizando o encoding padrão do sistema
                //StreamReader reader = new StreamReader(@"C:\SeuArquivo.txt", Encoding.Default);
                //// Abre o arquivo utilizando o encoding UTF8
                //StreamReader reader = new StreamReader(@"C:\SeuArquivo.txt", Encoding.UTF8);
                //// Abre o arquivo utilizando o encoding ASCII
                //StreamReader reader = new StreamReader(@"C:\SeuArquivo.txt", Encoding.ASCII);

                StreamReader srAquivo;

                string sArquivo;
                //srAquivo = File.OpenWrite(pCaminhoDoArquivo);

                Encoding codificacao = Encoding.Default;

                switch (pTipoDaCodificacao)
                {
                    case TipoDaCodificaoDoArquivo.ASCII:
                        codificacao = Encoding.ASCII;
                        break;

                    case TipoDaCodificaoDoArquivo.UTF7:
                        codificacao = Encoding.UTF7;
                        break;

                    case TipoDaCodificaoDoArquivo.UTF8:
                        codificacao = Encoding.UTF8;
                        break;

                    case TipoDaCodificaoDoArquivo.UTF32:
                        codificacao = Encoding.UTF32;
                        break;

                    case TipoDaCodificaoDoArquivo.Unicode:
                        codificacao = Encoding.Unicode;
                        break;

                    case TipoDaCodificaoDoArquivo.BigEndianUnicode:
                        codificacao = Encoding.BigEndianUnicode;
                        break;
                }


                srAquivo = new StreamReader(pCaminhoDoArquivo, codificacao);
                sArquivo = srAquivo.ReadToEnd();
                srAquivo.Close();

                retorno = sArquivo;

            }

            return retorno;
        }

        public static bool ArquivoExiste(string pArquivo)
        {
            bool retorno = true;

            try
            {
                if (File.Exists(pArquivo) == false)
                {
                    retorno = false;

                }
            }
            catch (Exception ex)
            {

                retorno = false;
            }

            return retorno;

        }

        public static bool AtualizarArquivoJson(string caminho, string json)
        {
            bool retorno = true;

            try
            {
                File.WriteAllText(caminho, json);
            }
            catch (Exception ex)
            {

                retorno = false;
            }

            return retorno;
        }

        public static bool ArquivoApagar_Unico(string pArquivo)
        {
            bool retorno = true;

            if (File.Exists(pArquivo) == true)
            {
                try
                {
                    File.Delete(pArquivo);

                }
                catch (Exception ex)
                {

                    retorno = false;
                }
            }

            return retorno;

        }

        public static string Arquivo_ApagarVariosDeUmDiretorio(string pDiretorio, string extensao = "*.*", string pQueNaoContenhaEsseTexto = "", string pApenasArquivosQueContenha = "")
        {

            if (!DiretorioExiste(pDiretorio))
            {
                return string.Empty;
            }

            string pastaTempo = pDiretorio;
            string erros = string.Empty;

            bool apaga = true;

            try
            {
                string[] arrStr = { };

                arrStr = System.IO.Directory.GetFiles(pastaTempo, extensao);

                for (int i = 0; i <= arrStr.Length - 1; i++)
                {
                    apaga = true; //Sempre exclui a nao ser que atenda uma das excessoes abaixo;

                    try
                    {

                        if (pQueNaoContenhaEsseTexto != string.Empty)
                        {
                            if (arrStr[i].IndexOf(pQueNaoContenhaEsseTexto) > -1)
                            {
                                apaga = false;
                            }
                        }

                        if (pApenasArquivosQueContenha != string.Empty)
                        {

                            if (arrStr[i].IndexOf(pApenasArquivosQueContenha) == -1)
                            {
                                apaga = false;
                            }
                        }

                        if (apaga == true)
                        {
                            System.IO.File.Delete(arrStr[i]);
                        }

                    }
                    catch (Exception ex)
                    {
                        if (erros != string.Empty) erros += "<br>";
                        erros += ex.Message;
                    }

                }

            }
            catch (IOException ex)
            {
                if (erros != string.Empty) erros += "<br>";
                erros += ex.Message;

            }

            return erros;
        }

        public static string ArquivoMover(string pCaminhoOriginal, string pCaminhoNovo, bool pApagarCaminhoNovoSeJaExistir = true)
        {
            string retorno = string.Empty;

            if (pCaminhoOriginal == string.Empty)
            {
                retorno = "Arquivo para mover não informado.";
            }
            else
            {
                if (ArquivoExiste(pCaminhoOriginal) == false)
                {
                    retorno = "Arquivo que deseja mover não existe: " + pCaminhoOriginal;
                }
            }

            if (retorno == string.Empty)
            {
                if (pCaminhoNovo == string.Empty)
                {
                    retorno = "Caminho novo para mover o arquivo não informado.";
                }
                else
                {
                    if (ArquivoExiste(pCaminhoNovo) == true)
                    {

                        if (pApagarCaminhoNovoSeJaExistir == false)
                        {

                            retorno = "Caminho novo para mover o arquivo já existe: " + pCaminhoNovo;
                        }
                        else
                        {
                            try
                            {
                                ArquivoApagar_Unico(pCaminhoNovo);

                            }
                            catch (Exception ex)
                            {
                                retorno = "Caminho novo para mover o arquivo já existe: " + pCaminhoNovo
                                     + " \n Não foi possível exclui-lo para mover o arquivo original: \n"
                                     + pCaminhoOriginal;
                            }

                        }
                    }
                }

            }

            if (retorno == string.Empty)
            {
                try
                {
                    File.Move(pCaminhoOriginal, pCaminhoNovo);
                }
                catch (Exception ex)
                {
                    retorno = "Não foi possível mover o arquivo atual: \n" + pCaminhoOriginal
                                     + " \n Para o caminho novo: \n" + pCaminhoNovo
                                     + "\n\n Erro: " + ex.Message;
                }
            }

            return retorno;

        }

        public static string ArquivoCriar(string pCaminhoCompletoDoArquivo, string pConteudoDoArquivo, bool pExcluirSeJaExisti = false, TipoDaCodificaoDoArquivo pTipoDaCodificacao = TipoDaCodificaoDoArquivo.Default)
        {
            string retorno = string.Empty;

            try
            {

                if (File.Exists(pCaminhoCompletoDoArquivo) == true) //Se já existi
                {
                    File.Delete(pCaminhoCompletoDoArquivo);//Exclui o arquivo
                }

                Encoding codificacao = Encoding.Default;

                switch (pTipoDaCodificacao)
                {
                    case TipoDaCodificaoDoArquivo.ASCII:
                        codificacao = Encoding.ASCII;
                        break;

                    case TipoDaCodificaoDoArquivo.UTF7:
                        codificacao = Encoding.UTF7;
                        break;

                    case TipoDaCodificaoDoArquivo.UTF8:
                        codificacao = Encoding.UTF8;
                        break;

                    case TipoDaCodificaoDoArquivo.UTF32:
                        codificacao = Encoding.UTF32;
                        break;

                    case TipoDaCodificaoDoArquivo.Unicode:
                        codificacao = Encoding.Unicode;
                        break;

                    case TipoDaCodificaoDoArquivo.BigEndianUnicode:
                        codificacao = Encoding.BigEndianUnicode;
                        break;
                }


                //Environment.NewLineiar arquivo
                StreamWriter wr = new StreamWriter(pCaminhoCompletoDoArquivo, false, codificacao);
                wr.Write(pConteudoDoArquivo);
                wr.Close();

                retorno = pCaminhoCompletoDoArquivo;//Se Environment.NewLineiar o arquivo o retorno é exatamente o caminho completo do arquivo
                //Senão Environment.NewLineiar o retorno é vazio
            }
            catch (Exception ex)
            {

            }

            return retorno;

        }

        public static string ArquivoRetornarNome(string pCaminhoCompleto, bool pArquivoPrecisaExistir = true, bool pRetornarTambemExtensao = true)
        {
            string retorno = string.Empty;
            string msg = string.Empty;

            pCaminhoCompleto = pCaminhoCompleto.Trim();

            if (pCaminhoCompleto != string.Empty)
            {
                msg = "";
                if (pArquivoPrecisaExistir == true)
                {
                    if (ArquivoExiste(pCaminhoCompleto) == false)
                    {
                        msg = "Arquivo informado não existe:" + Environment.NewLine + pCaminhoCompleto;
                    }
                }

                if (Tc.Vazio(msg))
                {
                    try
                    {
                        string aux = string.Empty;
                        int pos = pCaminhoCompleto.LastIndexOf(@"\");

                        if (pos > -1)
                        {
                            aux = pCaminhoCompleto.Substring(pos + 1);
                        }

                        if (aux != string.Empty)
                        {

                            if (!pRetornarTambemExtensao)
                            {
                                aux = ArquivoRemoverExtensao(aux, pArquivoPrecisaExistir);

                            }

                            retorno = aux;

                        }


                    }
                    catch (Exception ex)
                    {

                        msg = "Erro: " + ex.Message;
                    }
                }

            }
            else
            {
                msg = "Arquivo não informado!";
            }

            return retorno;

        }

        public static string ArquivoOrdemCompraRetornaNome(int pLayout, string pNomeUsuario, long pNumeroOrdemCompra)
        {
            var arquivo = $"{pNomeUsuario}_OrdemDeCompra_Numero{pNumeroOrdemCompra}_{pLayout}.pdf";

            return arquivo;
        }

        public static string ArquivoRemoverExtensao(string pCaminhoCompleto, bool pArquivoPrecisaExistir = true)
        {
            string retorno = string.Empty;
            string msg = string.Empty;

            pCaminhoCompleto = pCaminhoCompleto.Trim();

            if (pCaminhoCompleto != string.Empty)
            {
                msg = "";
                if (pArquivoPrecisaExistir == true)
                {
                    if (ArquivoExiste(pCaminhoCompleto) == false)
                    {
                        msg = "Arquivo informado não existe:" + Environment.NewLine + pCaminhoCompleto;
                    }
                }

                if (Tc.Vazio(msg))
                {
                    try
                    {
                        string aux = string.Empty;
                        aux = pCaminhoCompleto;
                        int pos = aux.LastIndexOf(".");//Pega o ultimo ponto do nome do arquivo

                        if (pos > -1)//Se encontrou remove 
                        {
                            aux = aux.Remove(pos);
                        }

                        retorno = aux;

                    }
                    catch (Exception ex)
                    {

                        msg = "Erro: " + ex.Message;
                    }
                }

            }
            else
            {
                msg = "Arquivo não informado!";
            }

            return retorno;

        }

        public static string ArquivoRetornarPasta(string pCaminhoCompleto, bool pArquivoPrecisaExistir = true)
        {
            string retorno = string.Empty;
            string msg = string.Empty;

            pCaminhoCompleto = pCaminhoCompleto.Trim();

            if (pCaminhoCompleto != string.Empty)
            {
                msg = "";
                if (pArquivoPrecisaExistir == true)
                {
                    if (ArquivoExiste(pCaminhoCompleto) == false)
                    {
                        msg = "Arquivo informado não existe:" + Environment.NewLine + pCaminhoCompleto;
                    }
                }

                if (Tc.Vazio(msg))
                {
                    try
                    {
                        string PASTA = ArquivoRetornarNome(pCaminhoCompleto, false);
                        PASTA = pCaminhoCompleto.Replace(PASTA, "");

                        if (PASTA != string.Empty)
                        {
                            retorno = PASTA;
                        }

                    }
                    catch (Exception ex)
                    {

                        msg = "Erro: " + ex.Message;
                    }
                }

            }
            else
            {
                msg = "Arquivo não informado!";
            }

            return retorno;

        }

        public static string ArquivocriarApartirDeBytes(string pCaminhoCompletoDoArquivo, byte[] pConteudoDoArquivoEmBytes)
        {
            string retorno = string.Empty;
            try
            {
                // Abre o arquivo para esEnvironment.NewLineita, se nao existir Environment.NewLineiar o arquivo vazio
                System.IO.FileStream fsArquivo =
                   new System.IO.FileStream(pCaminhoCompletoDoArquivo, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                //EsEnvironment.NewLineeve o conteudo binario no arquivo
                fsArquivo.Write(pConteudoDoArquivoEmBytes, 0, pConteudoDoArquivoEmBytes.Length);

                //Fecha o arquivo
                fsArquivo.Close();

            }
            catch (Exception ex)
            {

                retorno = "Ocorreu um erro ao Environment.NewLineiar o arquivo." + Environment.NewLine + ex.Message;
            }

            return retorno;
        }

        public static byte[] ArquivoRetornarEmBytes(string pCaminhoCompletoDoArquivo)
        {
            String strFilePath = pCaminhoCompletoDoArquivo;

            FileStream fsFile = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
            Byte[] arqBytes = new Byte[fsFile.Length];
            fsFile.Read(arqBytes, 0, arqBytes.Length);
            fsFile.Close();

            return arqBytes;
        }

        private static string retiraAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        public static string LimpaTexto(string strTexto)
        {
            string tmp = strTexto;

            if ((tmp != null) || (tmp != string.Empty))
            {
                tmp = tmp.Replace(".", string.Empty);
                tmp = tmp.Replace("-", string.Empty);
                tmp = tmp.Replace("/", string.Empty);
                tmp = tmp.Replace("(", string.Empty);
                tmp = tmp.Replace(")", string.Empty);
                tmp = tmp.Replace("*", string.Empty);
                tmp = tmp.Replace("@", string.Empty);
                tmp = tmp.Replace("#", string.Empty);
                tmp = tmp.Replace("$", string.Empty);
                tmp = tmp.Replace("&", string.Empty);
                tmp = tmp.Replace("$", string.Empty);
                tmp = tmp.Replace("~", string.Empty);
                tmp = tmp.Replace("^", string.Empty);
                tmp = tmp.Replace("`", string.Empty);
                tmp = tmp.Replace("´", string.Empty);
            }

            return retiraAcentos(tmp.Trim());
        }

        public static string CortaTexto(string texto, int digitos)
        {
            if (texto.Length > digitos)
                return texto.Substring(0, (digitos - 1));
            else
                return texto;
        }

        //Retorna uma quantidade de caracteres a esquerda de uma string
        public static string Esquerda(string pConteudo, int pTamanho)
        {
            string retorno = string.Empty;

            pConteudo = pConteudo.PadLeft(pTamanho, ' ');

            if (pConteudo.Length > 0)
            {
                if (pTamanho > 0)
                {
                    if (pConteudo.Length >= pTamanho)
                    {
                        retorno = pConteudo.Substring(0, pTamanho);
                    }
                    else
                    {
                        retorno = pConteudo;
                    }
                }
                else
                {
                    retorno = pConteudo;
                }
            }

            return retorno;

        }

        //Retorna uma quantidade de caracteres a direita de uma string
        public static string Direita(string pConteudo, int pTamanho)
        {
            string retorno = string.Empty;

            pConteudo = pConteudo.PadRight(pTamanho, ' ');

            if (pConteudo.Length > 0)
            {
                if (pTamanho > 0)
                {
                    retorno = pConteudo.Substring(pConteudo.Length - pTamanho, pTamanho);
                }
            }

            return retorno;

        }

        public static string FormataValor(string valor, int decimais)
        {
            if (valor.Trim() == string.Empty) valor = "0";

            string tmp = string.Empty;
            try
            {
                for (int x = 0; x < decimais; x++)
                    tmp += "0";
                return String.Format("{0:0." + tmp + "}", Convert.ToDouble(valor.Replace(".", ","))).Replace(",", ".");
            }
            catch (Exception ex)
            {
                return valor;
            }
        }


        public static string FormatarTelefone(string telefone)
        {

            string strMascara = "{0:00-0000-0000}";

            long lngNumero = Convert.ToInt64(Tc.SomenteNumeros(telefone));

            if (telefone.Length == 11)
                strMascara = "{0:00-00000-0000}";

            return string.Format(strMascara, lngNumero);
        }

        public static string DataExtensa(string data)
        {
            var dia = data.Substring(0, 2);
            var mes = data.Substring(3, 2);
            var ano = data.Substring(6, 4);

            switch (mes)
            {
                case "01":
                    mes = "Janeiro";
                    break;
                case "02":
                    mes = "Fevereiro";
                    break;
                case "03":
                    mes = "Março";
                    break;
                case "04":
                    mes = "Abril";
                    break;
                case "05":
                    mes = "Maio";
                    break;
                case "06":
                    mes = "Junho";
                    break;
                case "07":
                    mes = "Julho";
                    break;
                case "08":
                    mes = "Agosto";
                    break;
                case "09":
                    mes = "Setembro";
                    break;
                case "10":
                    mes = "Outubro";
                    break;
                case "11":
                    mes = "Novembro";
                    break;
                case "12":
                    mes = "Dezembro";
                    break;
            }

            return $"{dia} de {mes} de {ano}";
        }

        public static string CoversaoBinarioParaTexto(byte[] arquivo)
        {
            var retorno = string.Empty;

            var arquivoSR = new StreamReader(new MemoryStream(arquivo));
            retorno = arquivoSR.ReadToEnd();
            arquivoSR.Close();

            return retorno;
        }
    }
}
