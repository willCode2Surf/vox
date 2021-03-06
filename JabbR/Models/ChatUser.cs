﻿using JabbR.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JabbR.Models
{
    public class ChatUser
    {
        [Key]
        public int Key { get; set; }

        [MaxLength(200)]
        public string Id { get; set; }
        public string Name { get; set; }
        // MD5 email hash for gravatar
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public DateTime LastActivity { get; set; }
        public DateTime? LastNudged { get; set; }
        public int Status { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        [StringLength(200)]
        public string AfkNote { get; set; }

        public bool IsAfk { get; set; }

        [StringLength(255)]
        public string Flag { get; set; }

        // TODO: Migrate everyone off identity and email
        public string Identity { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; }

        // Request password reset token
        public string RequestPasswordResetId { get; set; }
        public DateTimeOffset? RequestPasswordResetValidThrough { get; set; }

        // List of clients that are currently connected for this user
        public virtual ICollection<ChatUserIdentity> Identities { get; set; }

        public string RawPreferences { get; set; }
        public virtual ICollection<ChatUserMention> Mentions { get; set; }
        public virtual ICollection<ChatClient> ConnectedClients { get; set; }
        public virtual ICollection<ChatRoom> OwnedRooms { get; set; }
        public virtual ICollection<ChatRoom> Rooms { get; set; }
        public virtual ICollection<ChatRoomUserData> RoomUserData { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

        // Private rooms this user is allowed to go into
        public virtual ICollection<ChatRoom> AllowedRooms { get; set; }

        public ChatUser()
        {
            Identities = new SafeCollection<ChatUserIdentity>();
            Mentions = new SafeCollection<ChatUserMention>();
            ConnectedClients = new SafeCollection<ChatClient>();
            OwnedRooms = new SafeCollection<ChatRoom>();
            Rooms = new SafeCollection<ChatRoom>();
            RoomUserData = new SafeCollection<ChatRoomUserData>();
            AllowedRooms = new SafeCollection<ChatRoom>();
            Attachments = new SafeCollection<Attachment>();
            Notifications = new SafeCollection<Notification>();
        }

        public bool HasUserNameAndPasswordCredentials()
        {
            return !String.IsNullOrEmpty(HashedPassword) && !String.IsNullOrEmpty(Name);
        }

        [NotMapped]
        public ChatUserPreferences Preferences
        {
            get
            {
                return ChatUserPreferences.GetPreferences(this);
            }

            set
            {
                value.Serialize(this);
            }
        }
    }
}
