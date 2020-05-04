
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
        [Display(Name = "Default")]
        Default = 0,

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