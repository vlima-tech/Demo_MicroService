
using System;
using System.ComponentModel.DataAnnotations;

namespace Praticis.Framework.Worker.Abstractions.Enums
{
    /// <summary>
    /// Represents work status types.
    /// </summary>
    [Flags]
    public enum WorkStatus
    {
        /*
            ## ATENÇÃO!! ##

            => Os Status de 1 a 4 possuem ordem lógica de execução 
            (não faz sentido estar como Processed e ser mudado para Created).

            => A classe Work possui uma lógica no método ChangeStatusTo() para
            evitar status inconsistentes, alterar os códigos destes Enums pode 
            gerar inconsistência na integridade dos dados na tabela Works
        */

        /// <summary>
        /// Is the first step. The work exists in database, is registered for execution 
        /// but there are many others and by performance decision will be loaded to memory 
        /// after others are being executed.
        /// </summary>
        Created = 1,

        /// <summary>
        /// Is the second step. The work has been loaded and is already in memory, 
        /// waiting your execution time.
        /// </summary>
        Enqueued = 2,

        /// <summary>
        /// Is the third step. The work is running.
        /// </summary>
        Processing = 3,

        /// <summary>
        /// Is the fourth and last step. The work was done.
        /// </summary>
        Processed = 4,

        /// <summary>
        /// This status is verified by pipeline. If has domain notifications, pipeline module
        /// will be change work status to failed.
        /// </summary>
        Failed = 5,

        /// <summary>
        /// Represents a work that was canceled by system user.
        /// </summary>
        Canceled = 6,

        /// <summary>
        /// Represents a work that has your execution stoped, probably by stopping the server.
        /// </summary>
        Stoped = 7
    }
}