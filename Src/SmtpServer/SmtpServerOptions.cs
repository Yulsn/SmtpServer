﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using SmtpServer.Authentication;
using SmtpServer.Storage;

namespace SmtpServer
{
    internal sealed class SmtpServerOptions : ISmtpServerOptions
    {
        readonly Collection<IPEndPoint> _endpoints = new Collection<IPEndPoint>();
        readonly Collection<IMailboxFilterFactory> _mailboxFilterFactories = new Collection<IMailboxFilterFactory>();

        /// <summary>
        /// Gets or sets the maximum size of a message.
        /// </summary>
        public int MaxMessageSize { get; internal set; }

        /// <summary>
        /// Gets or sets the SMTP server name.
        /// </summary>
        public string ServerName { get; internal set; }

        /// <summary>
        /// Gets the Server Certificate to use when starting a TLS session.
        /// </summary>
        public X509Certificate ServerCertificate { get; internal set; }

        /// <summary>
        /// Gets or sets the endpoint to listen on.
        /// </summary>
        internal Collection<IPEndPoint> Endpoints
        {
            get { return _endpoints; }
        }

        /// <summary>
        /// Gets or sets the endpoint to listen on.
        /// </summary>
        IReadOnlyCollection<IPEndPoint> ISmtpServerOptions.Endpoints
        {
            get { return new ReadOnlyCollection<IPEndPoint>(_endpoints); }
        }

        /// <summary>
        /// Gets or sets the mailbox filter factories to use.
        /// </summary>
        internal Collection<IMailboxFilterFactory> MailboxFilterFactories
        {
            get { return _mailboxFilterFactories; }
        }

        /// <summary>
        /// Gets the message store factory to use.
        /// </summary>
        public IMessageStoreFactory MessageStoreFactory { get; internal set; }

        /// <summary>
        /// Gets the mailbox filter factory to use.
        /// </summary>
        public IMailboxFilterFactory MailboxFilterFactory
        {
            get
            {
                if (_mailboxFilterFactories.Count == 1)
                {
                    return _mailboxFilterFactories.First();
                }

                return new CompositeMailboxFilterFactory(_mailboxFilterFactories.ToArray());
            }
        }

        /// <summary>
        /// Gets the user authenticator.
        /// </summary>
        public IUserAuthenticator UserAuthenticator { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether authentication should be allowed on an unsecure session.
        /// </summary>
        public bool AllowUnsecureAuthentication { get; internal set; }
    }
}
