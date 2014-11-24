﻿namespace FakeItEasy
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FakeItEasy.Core;

    /// <summary>
    /// A class exposing an event handler to attach to an event of a faked object
    /// in order to raise that event.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of the event args.</typeparam>
    public class Raise<TEventArgs>
        : IEventRaiserArguments where TEventArgs : EventArgs
    {
        private readonly TEventArgs eventArguments;
        private readonly object sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="Raise{TEventArgs}"/> class.
        /// </summary>
        /// <param name="sender">The sender of the event, or <c>null</c> if the fake is to be used as sender.</param>
        /// <param name="e">The event data.</param>
        /// <param name="argumentProviderMap">A map from event handlers to supplied arguments to use when raising.</param>
        internal Raise(object sender, TEventArgs e, EventHandlerArgumentProviderMap argumentProviderMap)
        {
            this.sender = sender;
            this.eventArguments = e;

            argumentProviderMap.AddArgumentProvider((EventHandler<TEventArgs>)this, this);
        }

        /// <summary>
        /// Converts a raiser into an <see cref="EventHandler{TEventArgs}"/>
        /// </summary>
        /// <param name="raiser">The raiser to convert.</param>
        /// <returns>The new event handler</returns>
        [SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Provides the event raising syntax.")]
        public static implicit operator EventHandler<TEventArgs>(Raise<TEventArgs> raiser)
        {
            return raiser.Now;
        }

        /// <summary>
        /// Converts a raiser into an <see cref="EventHandler"/>
        /// </summary>
        /// <param name="raiser">The raiser to convert.</param>
        /// <returns>The new event handler</returns>
        [SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Provides the event raising syntax.")]
        public static implicit operator EventHandler(Raise<TEventArgs> raiser)
        {
            return raiser.Now;
        }

        object[] IEventRaiserArguments.GetEventArguments(object fake)
        {
            return new[] { this.sender ?? fake, this.eventArguments };
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "sender", Justification = "Unused parameter.")]
        private void Now(object sender, TEventArgs e)
        {
            throw new NotSupportedException(ExceptionMessages.NowCalledDirectly);
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "sender", Justification = "Unused parameter.")]
        private void Now(object sender, EventArgs e)
        {
            throw new NotSupportedException(ExceptionMessages.NowCalledDirectly);
        }
    }
}