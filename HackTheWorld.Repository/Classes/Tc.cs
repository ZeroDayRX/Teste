using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Api.Repository.Classes
{

    public enum enPosicaoDosZeros
    {
        esquerda = 0,
        direita = 1
    }

    public static class Tc
    {
        public static string gSeparadorPadraoDeString = "#";
        public static long SPL(string pValor)
        {
            long ret = 0;

            if (pValor != string.Empty)
            {
                if (ValidarNumeros(pValor) == true)
                {
                    ret = Convert.ToInt64(pValor);
                }
            }

            return ret;
        }
        public static bool Vazio(object pValor, bool pConsiderarEspacosEmBrancoComoVazio = false)
        {
            bool ret = false;


            if (pValor == null)//É nulo
            {
                ret = true;
            }
            else//Não é nulo
            {
                if (pValor == string.Empty)//Esta vazio
                {
                    ret = true;
                }
                else
                {
                    if (pConsiderarEspacosEmBrancoComoVazio == true)
                    {
                        string aux = string.Empty;

                        aux = pValor.ToString();
                        aux = aux.Trim();

                        if (aux == string.Empty)//Esta vazio
                        {
                            ret = true;
                        }

                    }

                }

            }

            return ret;
        }
        public static string Esq(string pValor, int pQtde)
        {
            string retorno = pValor;


            if (!Vazio(retorno))
            {
                if (pValor.Length >= pQtde)
                {
                    retorno = pValor.Substring(0, pQtde);
                }
            }

            return retorno;
        }
        public static string QuebraFrases(string pValor, int pQtde)
        {
            string retorno = pValor;
            int pos = 0;
            int pos2 = 0;
            string quebra = string.Empty;
            string ret = "";
            int tamanho = pQtde;

            pos2 += pos;

            if (!Vazio(retorno))
            {
                while (pValor.Length >= pos2)
                {
                    if ((pos2 + pQtde) > pValor.Length)
                    {
                        retorno = pValor.Substring(pos2, pValor.Length - pos2);
                        ret += retorno;
                        return ret;
                    }
                    else
                        retorno = pValor.Substring(pos2, pQtde);

                    pos = retorno.Length;
                    quebra = retorno.Substring(pos - 1, 1);

                    if (quebra != " ")
                    {
                        for (int i = pQtde; i >= 1; i--)
                        {
                            retorno = retorno.Substring(0, i);
                            pos = retorno.Length;
                            quebra = retorno.Substring(pos - 1, 1);

                            if (quebra == " ")
                            {
                                ret += retorno + "#";
                                //tamanho = pos;
                                i = 0;
                            }
                        }
                    }
                    else
                        ret += retorno + "#";

                    pos2 += pos;
                    //tamanho += pQtde;
                }
            }

            return retorno;
        }
        public static string Dir(string pValor, int pQtde)
        {
            string retorno = pValor;

            Int32 posIni = 0;

            if (!Vazio(retorno))
            {
                if (pValor.Length >= pQtde)
                {
                    posIni = (pValor.Length - pQtde);
                    retorno = pValor.Substring(posIni);
                }
            }

            return retorno;
        }
        public static Int32 SPI(object pValor)
        {
            Int32 ret = 0;

            if (Tc.OPS(pValor) != string.Empty)
            {
                if (ValidarNumeros(pValor.ToString()) == true)
                {
                    ret = Convert.ToInt32(pValor.ToString());
                }
            }

            return ret;
        }
        public static Decimal SPD(string pValor, string pSeparadorDeDecimalOriginalDoCampo = ",", string pSeparadorDeMilharOriginalDoCampo = "")
        {
            Decimal ret = 0;

            if (pValor != string.Empty)
            {

                if (!Vazio(pSeparadorDeMilharOriginalDoCampo))
                {
                    if (pSeparadorDeMilharOriginalDoCampo != pSeparadorDeDecimalOriginalDoCampo)
                    {
                        pValor = pValor.Replace(pSeparadorDeMilharOriginalDoCampo, "");
                    }

                }

                if (!Vazio(pSeparadorDeDecimalOriginalDoCampo))
                {
                    if (pSeparadorDeDecimalOriginalDoCampo != pSeparadorDeMilharOriginalDoCampo)
                    {
                        if (pSeparadorDeDecimalOriginalDoCampo != ",")
                        {
                            pValor = pValor.Replace(pSeparadorDeDecimalOriginalDoCampo, ",");
                        }
                    }
                }


                if (ValidarNumeros(pValor) == true)
                {
                    ret = Convert.ToDecimal(pValor);
                }
            }

            return ret;
        }
        public static Double SPDbl(string pValor, string pSeparadorDeDecimalOriginalDoCampo = ",", string pSeparadorDeMilharOriginalDoCampo = "")
        {
            Double ret = 0;

            if (pValor != string.Empty)
            {

                if (!Vazio(pSeparadorDeMilharOriginalDoCampo))
                {
                    if (pSeparadorDeMilharOriginalDoCampo != pSeparadorDeDecimalOriginalDoCampo)
                    {
                        pValor = pValor.Replace(pSeparadorDeMilharOriginalDoCampo, "");
                    }

                }

                if (!Vazio(pSeparadorDeDecimalOriginalDoCampo))
                {
                    if (pSeparadorDeDecimalOriginalDoCampo != pSeparadorDeMilharOriginalDoCampo)
                    {
                        if (pSeparadorDeDecimalOriginalDoCampo != ",")
                        {
                            pValor = pValor.Replace(pSeparadorDeDecimalOriginalDoCampo, ",");
                        }
                    }
                }


                if (ValidarNumeros(pValor) == true)
                {
                    ret = Convert.ToDouble(pValor);
                }
            }

            return ret;
        }
        public static bool OPB(object pValor)
        {
            bool ret = false;

            ret = Convert.ToBoolean(pValor);

            return ret;
        }
        public static string IPS(object pValor)
        {
            string ret = "";
            if (Tc.SPI(Tc.OPS(pValor)) > 0)
            {
                ret = pValor.ToString();
            }
            return ret;
        }
        //CHECKBOX PARA BOLEANO
        public static bool CPB(object pValor)
        {
            bool ret = false;

            ret = Convert.ToBoolean(pValor);

            return ret;
        }

        //OBJETO PARA STRING
        public static string OPS(object pValor, bool pAplicarTRIM = true, string pRetornoParaNulos_E_Vazios = "", bool pTrocarCaracatesEspeciais = false)
        {
            string ret = string.Empty;

            try
            {
                ret = Convert.ToString(pValor);

                if (pAplicarTRIM == true)
                {

                    if (pValor == null)
                    {
                        pValor = "";
                    }

                    if (pValor.GetType().FullName == "System.String")
                    {
                        ret = ret.Trim();

                    }
                }
            }
            catch
            {
                ret = string.Empty;
            }


            if (Vazio(ret))
            {
                ret = pRetornoParaNulos_E_Vazios;

                if (pTrocarCaracatesEspeciais)
                {

                }
            }

            return ret;
        }

        //OBJETO PARA MOEDA
        public static string OPM(object pValor, int pQtdeDecimais = 2, string pSeparadorDecimalOriginalDoCampo = ",", string pSeparadorDeMilharOriginalDoCampo = "", string pNoRetornoMostrarDecimalComo = ",")
        {
            decimal valor = 0;
            string retorno = "0";
            try
            {

                retorno = TratarDecimais(pValor.ToString(), pQtdeDecimais, pNoRetornoMostrarDecimalComo, pSeparadorDeMilharOriginalDoCampo, pSeparadorDecimalOriginalDoCampo);

            }
            catch
            {
                retorno = "0";
            }

            return retorno;
        }

        public static string OPM(string pValor)
        {
            string retorno = "0";
            try
            {
                retorno = string.Format("{0:0,0.00}", Tc.SPD(pValor));
            }
            catch
            {
                retorno = "0,00";
            }

            return retorno;
        }

        //OBJETO PARA NUMERO INTEIRO
        public static string OPN(object pValor, bool pRetiraCaracteresEspeciaisSeHouver = true, string pSubtituirRetornoNuloPor = "0", int pCompletarComZerosAEsquerda = 0)
        {

            string retorno = "0";
            try
            {

                retorno = OPS(pValor);

                if (pRetiraCaracteresEspeciaisSeHouver)
                {
                    retorno = RetirarCaracteresEspeciais(retorno);
                }

            }
            catch
            {
                retorno = "0";
            }


            if (Tc.Vazio(retorno))
            {
                if (!Tc.Vazio(pSubtituirRetornoNuloPor))
                {
                    retorno = pSubtituirRetornoNuloPor;
                }


            }

            if (pCompletarComZerosAEsquerda > 0)
            {
                retorno = CompletarComZeros(retorno, pCompletarComZerosAEsquerda);
            }



            return retorno;
        }

        //STRING para objeto
        public static object SPO(string pValor, bool pAplicarTRIM = true, string pRetornoParaNulos_E_Vazios = "", bool pTrocarCaracatesEspeciais = false)
        {
            object ret = null;

            try
            {
                ret = pValor;
            }
            catch
            {
                ret = null;
            }

            return ret;
        }

        public static string RetirarCaracteresEspeciais(string pValor)
        {
            //Precisa melhorar esse metodo
            string retorno = pValor;

            if (string.IsNullOrEmpty(retorno))
            {
                retorno = "";
            }

            retorno = retorno.Replace("-", "");
            retorno = retorno.Replace("/", "");
            retorno = retorno.Replace("á", "a");
            retorno = retorno.Replace("é", "é");
            retorno = retorno.Replace("í", "i");
            retorno = retorno.Replace("ó", "o");
            retorno = retorno.Replace("ã", "a");
            retorno = retorno.Replace("õ", "o");
            retorno = retorno.Replace("â", "a");
            retorno = retorno.Replace(".", "");

            return retorno;

        }

        public static string SomenteNumeros(object pValor, int pCompletarComZeros = 0)
        {

            string retorno = string.Empty; ;
            string valor = Convert.ToString(pValor);
            int i;

            if (valor == string.Empty)
            {
                retorno = "0";

            }

            if (valor.Length > 0)
            {

                for (i = 1; (i <= valor.Length); i = (i + 1))
                {
                    if ((("0123456789".IndexOf(valor.Substring((i - 1), 1), 0) + 1) > 0))
                    {
                        retorno = (retorno + valor.Substring((i - 1), 1));
                    }
                }

                if (retorno == string.Empty)
                {
                    retorno = "0";
                }

            }

            if (pCompletarComZeros > 0)
            {
                retorno = CompletarComZeros(retorno, pCompletarComZeros);
            }

            return retorno;

        }

        public static string SomenteAlfanumerico(object pValor, int pCompletarComZeros = 0, bool pAplicarTrim = true)
        {

            string retorno = string.Empty; ;
            string valor = Convert.ToString(pValor);
            int i;

            if (valor == string.Empty)
            {
                retorno = "0";

            }

            if (valor.Length > 0)
            {

                for (i = 1; (i <= valor.Length); i = (i + 1))
                {
                    if ((("0123456789abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ".IndexOf(valor.Substring((i - 1), 1), 0) + 1) > 0))
                    {
                        retorno = (retorno + valor.Substring((i - 1), 1));
                    }
                }

                if (retorno == string.Empty)
                {
                    retorno = "0";
                }

            }

            if (pAplicarTrim == true)
            {
                retorno = retorno.Trim();
            }

            if (pCompletarComZeros > 0)
            {
                retorno = CompletarComZeros(retorno, pCompletarComZeros);
            }

            return retorno;

        }

        public static string TratarDecimais(string pValor, int pQtdeDecimais = 2, string pNoRetornoMostrarDecimalComo = ",", string pSeparadorDeMilharOriginalDoCampo = "", string pSeparadorDeDecimalOriginalDoCampo = ".")
        {

            string retorno = "0";
            string aux = "0.";
            string formato = string.Empty;
            string simbDecimalParaFuncao = ",";

            if (pValor != null)
            {
                if (ValidarNumeros(pValor, pSeparadorDeMilharOriginalDoCampo, pSeparadorDeDecimalOriginalDoCampo) == true)
                {

                    if (pQtdeDecimais > 0)
                    {
                        aux += string.Empty.PadRight(pQtdeDecimais, '0');
                    }

                    formato = "{0:" + aux + "}";



                    if (!Vazio(pSeparadorDeMilharOriginalDoCampo))
                    {
                        if (pSeparadorDeMilharOriginalDoCampo != pSeparadorDeDecimalOriginalDoCampo)
                        {
                            pValor = pValor.Replace(pSeparadorDeMilharOriginalDoCampo, "");
                            formato = "{0:N}";
                        }

                    }


                    if (!Vazio(pSeparadorDeDecimalOriginalDoCampo))
                    {
                        if (pSeparadorDeDecimalOriginalDoCampo != pSeparadorDeMilharOriginalDoCampo)
                        {
                            if (pSeparadorDeDecimalOriginalDoCampo != simbDecimalParaFuncao)
                            {
                                pValor = pValor.Replace(pSeparadorDeDecimalOriginalDoCampo, simbDecimalParaFuncao);
                            }
                        }
                    }



                    retorno = String.Format(formato, Convert.ToDecimal(pValor));

                }

                if (pNoRetornoMostrarDecimalComo != simbDecimalParaFuncao)
                {
                    retorno = retorno.Replace(simbDecimalParaFuncao, pNoRetornoMostrarDecimalComo);
                }
            }

            return retorno;
        }

        public static long RetValorLongo(string pValor)
        {

            string retorno = "0";
            string formato = string.Empty;

            if (ValidarNumeros(pValor) == true)
            {
                retorno = pValor;
            }

            if (retorno == string.Empty)
            {
                retorno = "0";
            }

            return Convert.ToInt64(retorno);

        }

        //Função para Tratar Data
        public static string RetData(string pValor, bool pSomenteData = false)
        {
            string data = pValor;
            string retorno = string.Empty;

            if (data == null)
            {
                data = string.Empty;
            }
            else
            {
                try
                {
                    if (ValidarDatas(data) == false)
                    {
                        data = string.Empty;
                    }
                    else
                    {
                        //0:d/M/yyyy HH:mm:ss}
                        if (pSomenteData == true)
                        {
                            data = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(data));
                        }
                        else
                        {
                            data = String.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(data));

                        }
                    }
                }
                catch
                {

                }
            }

            retorno = data;

            return retorno;

        }

        public static int RetValorInteiro(string pValor)
        {

            string retorno = "0";
            string formato = string.Empty;

            if (ValidarNumeros(pValor) == true)
            {
                retorno = pValor;
            }

            if (retorno == string.Empty)
            {
                retorno = "0";
            }

            return Convert.ToInt32(retorno);

        }

        public static double RetValorDuplo(string pValor)
        {

            string retorno = "0";
            string aux = "0.";
            string formato = string.Empty;

            if (ValidarNumeros(pValor) == true)
            {
                // pValor = pValor.Replace(",", ".");

                formato = "{0:" + aux + "}";

                retorno = String.Format(formato, Convert.ToDouble(pValor));


            }

            if (retorno == string.Empty)
            {
                retorno = "0";
            }

            return Convert.ToDouble(retorno);

        }

        // Função para Validar uma nota
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

        // Função para Validar um NUMERO
        public static bool ValidarNumeros(string pValor, string pSeparadorDeMilharOriginalDoCampo = "", string pSeparadorDeDecimalOriginalDoCampo = "")
        {
            bool retorno = true;

            double valorValido;

            if (pValor != null)
            {
                if (!Vazio(pSeparadorDeMilharOriginalDoCampo))
                {
                    if (pSeparadorDeMilharOriginalDoCampo != pSeparadorDeDecimalOriginalDoCampo)
                    {
                        pValor = pValor.Replace(pSeparadorDeMilharOriginalDoCampo, "");
                    }

                }

                if (!Vazio(pSeparadorDeDecimalOriginalDoCampo))
                {
                    if (pSeparadorDeDecimalOriginalDoCampo != pSeparadorDeMilharOriginalDoCampo)
                    {
                        if (pSeparadorDeDecimalOriginalDoCampo != ",")
                        {
                            pValor = pValor.Replace(pSeparadorDeDecimalOriginalDoCampo, ",");
                        }
                    }
                }


                if (!Double.TryParse(pValor, out valorValido))
                {
                    retorno = false;
                }
            }

            return retorno;
        }

        //Função para Validar EMAIL
        public static bool ValidarEmail(string pEmail)
        {
            string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (System.Text.RegularExpressions.Regex.IsMatch(pEmail, strModelo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static string BrancosEsq(string pValorOriginal, int pTamanhoDaStringFinal = 0, string pCompletarCom = " ", bool pTruncarSeValorOriginalForMaior = true)
        {
            string retorno = string.Empty;

            if (Vazio(pCompletarCom))// Se por algum motivo estiver vazio
            {
                pCompletarCom = " ";
            }
            else if (pCompletarCom.Length > 1)//Se tiver mais que um caracter
            {
                pCompletarCom = Tc.Esq(pCompletarCom, 1);
            }

            if (pValorOriginal.Length > pTamanhoDaStringFinal)//Se o tamanho do valor for maior que o tamanho final da string
            {
                if (pTruncarSeValorOriginalForMaior == true) //Se for para truncar quando maior
                {
                    pValorOriginal = Esq(pValorOriginal, pTamanhoDaStringFinal);//trunca
                }
            }

            retorno = CompletarString(pValorOriginal, pCompletarCom, pTamanhoDaStringFinal, enPosicaoDosZeros.esquerda);

            return retorno;
        }

        public static string BrancosDir(string pValorOriginal, int pTamanhoDaStringFinal = 0, string pCompletarCom = " ", bool pTruncarSeValorOriginalForMaior = true)
        {
            string retorno = string.Empty;

            if (Vazio(pCompletarCom))// Se por algum motivo estiver vazio
            {
                pCompletarCom = " ";
            }
            else if (pCompletarCom.Length > 1)//Se tiver mais que um caracter
            {
                pCompletarCom = Tc.Esq(pCompletarCom, 1);
            }

            if (pValorOriginal != null)
            {
                if (pValorOriginal.Length > pTamanhoDaStringFinal)//Se o tamanho do valor for maior que o tamanho final da string
                {
                    if (pTruncarSeValorOriginalForMaior == true) //Se for para truncar quando maior
                    {
                        pValorOriginal = Esq(pValorOriginal, pTamanhoDaStringFinal);//trunca
                    }
                }

                retorno = CompletarString(pValorOriginal, pCompletarCom, pTamanhoDaStringFinal, enPosicaoDosZeros.direita);
            }

            return retorno;
        }

        public static string BrancosCent(string pValorOriginal, int pTamanhoDaStringFinal = 0, string pCompletarCom = " ", bool pTruncarSeValorOriginalForMaior = true)
        {
            string retorno = string.Empty;
            string aux = string.Empty;
            int tamConteudo = 0;
            int tamEsq = 0;
            int tamDir = 0;
            int tamRest = 0;

            if (pValorOriginal == null || string.IsNullOrEmpty(pValorOriginal))
            {
                pValorOriginal = " ";
            }

            tamConteudo = pValorOriginal.Length; //Pega o tamanho do conteudo

            tamRest = pTamanhoDaStringFinal - tamConteudo;//Pega quanto falta para completar a string

            if (tamRest > 0) //Se faltar alguma coisa pra completar
            {
                tamEsq = tamRest / 2;//Descobre quantos caracteres precisa completar a esquerda da string
                tamDir = tamRest - tamEsq; //O que faltar joga para completar a direita
            }

            if (Vazio(pCompletarCom))// Se por algum motivo estiver vazio
            {
                pCompletarCom = " ";
            }
            else if (pCompletarCom.Length > 1)//Se tiver mais que um caracter
            {
                pCompletarCom = Tc.Esq(pCompletarCom, 1);
            }


            //aux = ("").PadLeft(tamEsq,Char.Parse(pCompletarCom));
            aux += pValorOriginal;
            if (tamDir > 0) //Se Tiver Tamanho a direita para completar
            {
                aux += ("").PadRight(tamDir, Char.Parse(pCompletarCom));
            }
            //retorno = aux;

            if (aux.Length > pTamanhoDaStringFinal)//Se o tamanho do valor for maior que o tamanho final da string
            {
                if (pTruncarSeValorOriginalForMaior == true) //Se for para truncar quando maior
                {
                    aux = Esq(aux, pTamanhoDaStringFinal);//trunca
                }
            }

            retorno = CompletarString(aux, pCompletarCom, pTamanhoDaStringFinal, enPosicaoDosZeros.esquerda);


            return retorno;
        }

        public static string CompletarString(string pValorOriginal, string pCompletarCom = " ", int pTamanhoDaStringFinal = 0, enPosicaoDosZeros pPosicaoDosZeros = enPosicaoDosZeros.esquerda)
        {

            string strObjForm = pValorOriginal;

            if (pTamanhoDaStringFinal > 0)
            {
                if (pPosicaoDosZeros == enPosicaoDosZeros.esquerda)
                {
                    strObjForm = strObjForm.PadLeft(pTamanhoDaStringFinal, char.Parse(pCompletarCom));
                }
                else
                {
                    strObjForm = strObjForm.PadRight(pTamanhoDaStringFinal, char.Parse(pCompletarCom));
                }
            }

            return strObjForm;

        }

        public static string CompletarComZeros(string pValor, int pQtdeDeZeros = 0, enPosicaoDosZeros pPosicaoDosZeros = enPosicaoDosZeros.esquerda)
        {

            string strObjForm = "";
            int i;

            for (i = 1; (i <= pValor.Length); i = (i + 1))
            {
                if ((("0123456789".IndexOf(pValor.Substring((i - 1), 1), 0) + 1) > 0))
                {
                    strObjForm = (strObjForm + pValor.Substring((i - 1), 1));
                }
            }

            if (strObjForm == string.Empty)
            {
                strObjForm = "0";
            }

            if (pQtdeDeZeros > 0)
            {
                if (pPosicaoDosZeros == enPosicaoDosZeros.esquerda)
                {
                    strObjForm = strObjForm.PadLeft(pQtdeDeZeros, '0');
                }
                else
                {
                    strObjForm = strObjForm.PadRight(pQtdeDeZeros, '0');
                }
            }

            return strObjForm;

        }

        public static string RetirarEntersTabs(string pConteudo, bool pRetirarEnters = true, bool pRetirarTabs = true, string pRetirarEssesSimbolos = "")
        {
            string retorno = string.Empty;
            retorno = pConteudo;
            if (pRetirarEnters)
            {
                retorno = retorno.Replace("\r", "");
                retorno = retorno.Replace("\n", "");
            }

            if (pRetirarTabs)
            {
                retorno = retorno.Replace("\t", "");
            }

            if (!Tc.Vazio(pRetirarEssesSimbolos))
            {
                retorno = retorno.Replace(pRetirarEssesSimbolos, "");
            }


            return retorno;
        }

        public static string QuebrarStringEmPartes(string pConteudoOriginal, int pTamanhoParaCadaParte, string pStringSeparadorDasPartes = "")
        {

            string retorno = string.Empty;
            string aux = string.Empty;
            string texto = string.Empty;
            int tamConteudo = 0;

            tamConteudo = pConteudoOriginal.Length; //Pega o tamanho do conteudo

            try
            {
                if (tamConteudo > pTamanhoParaCadaParte)
                {
                    if (Tc.Vazio(pStringSeparadorDasPartes))
                    {
                        pStringSeparadorDasPartes = gSeparadorPadraoDeString;
                    }

                    texto = pConteudoOriginal;
                    while (!Tc.Vazio(texto))
                    {
                        aux = Esq(texto, pTamanhoParaCadaParte);

                        if (!Tc.Vazio(aux))
                        {
                            if (!Tc.Vazio(retorno))
                            {
                                retorno += gSeparadorPadraoDeString;
                            }

                            retorno += aux;

                            texto = texto.Replace(aux, "");

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                //Util.msgBox(ex.Message, "Função: Tc.QuebrarStringEmPartes");

                string ArquivoLogConteudo = "";
                ArquivoLogConteudo += (ex.Message + Environment.NewLine);
                ArquivoLogConteudo += "pConteudoOriginal: " + pConteudoOriginal + Environment.NewLine;
                ArquivoLogConteudo += "pTamanhoParaCadaParte: " + pTamanhoParaCadaParte + Environment.NewLine;
                ArquivoLogConteudo += "pStringSeparadorDasPartes: " + pStringSeparadorDasPartes + Environment.NewLine;

                string ArquivoLogNome = "";
                ArquivoLogNome += "Tc.QuebrarStringEmPartes";
                //ArquivoLogNome += Util.FormatarDataHora(Util.ObterDataEHoradoDoSistema(enTipoRetornoDataHora.data_E_Hota), TipoDeData.DataHora_YYYYMMDDHHMMSS, false);
                ArquivoLogNome += ".txt";


                Util.ArquivoCriar(ArquivoLogNome, ArquivoLogConteudo);

            }
            return retorno;

        }

        public static string MascaraTELEFONE(string pTELEFONE)
        {
            //fone = ####-####
            //fone = #####-####
            //fone = (##)####-####
            //fone = (##)#####-####

            string aux1 = string.Empty;
            try
            {
                aux1 = pTELEFONE;
                if (!Tc.Vazio(aux1))
                {
                    aux1 = SomenteNumeros(aux1);

                    if (aux1.Length == 8)
                    {
                        aux1 = String.Format(@"{0:0000\-0000}", Tc.SPL(aux1));
                    }
                    else if (aux1.Length == 9)
                    {
                        aux1 = String.Format(@"{0:00000\-0000}", Tc.SPL(aux1));
                    }
                    else if (aux1.Length == 10)
                    {
                        aux1 = String.Format(@"{0:\(00\)0000\-0000}", Tc.SPL(aux1));
                    }
                    else if (aux1.Length == 11)
                    {
                        aux1 = String.Format(@"{0:\(00\)00000\-0000}", Tc.SPL(aux1));
                    }
                    else
                    {
                        aux1 = pTELEFONE; //Se nao for deixa como foi passado
                    }
                }
            }
            catch (Exception)
            {
                aux1 = "";
            }
            return aux1;
        }

        public static int SomenteInteiro(decimal valor)
        {
            var vetor = valor.ToString().Split('.');

            int resultado = Convert.ToInt32(vetor[0]);

            if (vetor.Length > 1 && Convert.ToInt32(vetor[1]) > 0)
            {
                resultado++;
            }

            return resultado;
        }

        public static string MascararNumero(long numero)
        {
            var mascara = string.Empty;
            var pedido = Tc.OPS(numero);

            var tamanho = pedido.Length;

            var data = DateTime.Now;
            string ano = Tc.OPS(data.Year);
            string[] vetorData = new string[8];

            vetorData[0] = Tc.CompletarComZeros(Tc.OPS(data.Day), 2).Substring(0, 2);
            vetorData[1] = Tc.CompletarComZeros(Tc.OPS(data.Month), 2).Substring(0, 2);
            vetorData[2] = Tc.OPS(ano.Substring(0, 2));
            vetorData[3] = Tc.OPS(ano.Substring(2, 2));
            vetorData[4] = Tc.CompletarComZeros(Tc.OPS(data.Hour), 2).Substring(0, 2);
            vetorData[5] = Tc.CompletarComZeros(Tc.OPS(data.Minute), 2).Substring(0, 2);
            vetorData[6] = Tc.CompletarComZeros(Tc.OPS(data.Second), 2).Substring(0, 2);
            vetorData[7] = Tc.CompletarComZeros(Tc.OPS(data.Millisecond), 2).Substring(0, 2);

            for (int i = 0; i < vetorData.Length; i++)
            {

                if (i < tamanho)
                {
                    mascara += $"{vetorData[i]}{pedido.Substring(tamanho - (i + 1), 1)}";
                }
                else
                {
                    mascara += $"{ vetorData[i]}0";
                }
            }

            return mascara;
        }

    }
}
