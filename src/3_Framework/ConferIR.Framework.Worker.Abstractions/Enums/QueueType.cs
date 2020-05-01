
using System;
using System.ComponentModel.DataAnnotations;

namespace Praticis.Framework.Worker.Abstractions.Enums
{
    /// <summary>
    /// Represents the existing queues.
    /// </summary>
    [Flags]
    public enum QueueType
    {
        /*
            ### ATENÇÃO!! ###

            => Renomear nomes destes Enums podem gerar mapeamento errado das fila,
                certifique que os Work Maps (classes que implementam IWorkConfiguration) 
                não foram influenciados;

            => É fundamental que os códigos das Filas (seção "Queues", em appsettings.json) sejam 
                equivalentes deste Enums, senão processos poderão ser enfileirados em lugares errados;
        */

        /// <summary>
        /// Fila Padrão, hospeda execução de Works que não se enquadrem em alguma fila existente.
        /// </summary>
        [Display(Name = "Default", ResourceType = typeof(Resources.QueueTypeResource))]
        Default = 0,

        /// <summary>
        /// Processa declarações do <strong>ANO ATUAL</strong>, com status relacionado a: 
        /// <strong>Recepcionada</strong>
        /// <strong>Aguardando Processamento</strong>
        /// </summary>
        [Display(Name = "Ecac_Current_Waiting_Process", ResourceType = typeof(Resources.QueueTypeResource))]
        Ecac_Current_Waiting_Process = 1,

        /// <summary>
        /// Processa declarações do <strong>ANO ATUAL</strong>, com status relacionado a: 
        /// <strong>Em Processamento</strong>
        /// </summary>
        [Display(Name = "Ecac_Current_Processing", ResourceType = typeof(Resources.QueueTypeResource))]
        Ecac_Current_Processing = 2,

        /// <summary>
        /// Processa declarações do <strong>ANO ATUAL</strong>, com status relacionado a: 
        /// <strong>Com Pendência</strong>
        /// </summary>
        [Display(Name = "Ecac_Current_Pending", ResourceType = typeof(Resources.QueueTypeResource))]
        Ecac_Current_Pending = 3,

        /// <summary>
        /// Processa declarações do <strong>ANO ATUAL</strong>, com status relacionado a: 
        /// <strong>Em Restituição</strong>
        /// </summary>
        [Display(Name = "Ecac_Current_Repayment", ResourceType = typeof(Resources.QueueTypeResource))]
        Ecac_Current_Repayment = 4,

        /// <summary>
        /// Processa declarações de <strong>ANOS ANTERIORES</strong>, com status relacionado a: 
        /// <strong>Recepcionada</strong>
        /// <strong>Aguardando Processamento</strong>
        /// <strong>Em Processamento</strong>
        /// </summary>
        [Display(Name = "Ecac_Previous_Waiting_Process", ResourceType = typeof(Resources.QueueTypeResource))]
        Ecac_Previous_Waiting_Process = 5,

        /// <summary>
        /// Processa declarações de <strong>ANOS ANTERIORES</strong>, com status relacionado a: 
        /// <strong>Com Pendência</strong>
        /// </summary>
        [Display(Name = "Ecac_Previous_Pending", ResourceType = typeof(Resources.QueueTypeResource))]
        Ecac_Previous_Pending = 6,

        /// <summary>
        /// Processa declarações de <strong>ANOS ANTERIORES</strong>, com status relacionado a: 
        /// <strong>Em Restituição</strong>
        /// </summary>
        [Display(Name = "Ecac_Previous_Repayment", ResourceType = typeof(Resources.QueueTypeResource))]
        Ecac_Previous_Repayment = 7,

        /// <summary>
        /// Processa declarações de <strong>ANOS ANTERIORES</strong>, com status relacionado a: 
        /// <strong>Processadas</strong>
        /// </summary>
        [Display(Name = "Ecac_All_Processed", ResourceType = typeof(Resources.QueueTypeResource))]
        Ecac_All_Processed = 8,

        /// <summary>
        /// Processa declarações nunca verificadas ou que não se enquadram em outras situações
        /// </summary>
        [Display(Name = "Ecac_All_Others", ResourceType = typeof(Resources.QueueTypeResource))]
        Ecac_All_Others = 9,

        /*
            ### ATENÇÃO!! ###

            => Renomear nomes destes Enums podem gerar mapeamento errado das fila,
                certifique que os Work Maps (classes que implementam IWorkConfiguration) 
                não foram influenciados;

            => É fundamental que os códigos das Filas (seção "Queues", em appsettings.json) sejam 
                equivalentes deste Enums, senão processos poderão ser enfileirados em lugares errados;
        */
    }
}