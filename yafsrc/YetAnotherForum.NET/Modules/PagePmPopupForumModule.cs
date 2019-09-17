/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2019 Ingo Herbote
 * http://www.yetanotherforum.net/
 *
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at

 * http://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
namespace YAF.Modules
{
    #region Using

    using System;

    using YAF.Core;
    using YAF.Dialogs;
    using YAF.Types;
    using YAF.Types.Attributes;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Types.Objects;

    #endregion

    /// <summary>
    /// The Page PM Popup Module
    /// </summary>
    [YafModule(moduleName: "Page PopUp Module", moduleAuthor: "Tiny Gecko", moduleVersion: 1)]
    public class PagePmPopupForumModule : SimpleBaseForumModule
    {
        #region Public Methods

        /// <summary>
        /// The init after page.
        /// </summary>
        public override void InitAfterPage()
        {
            this.CurrentForumPage.Load += this.ForumPageLoad;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The display received thanks popup.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool DisplayReceivedThanksPopup()
        {
            return this.PageContext.ReceivedThanks > 0;
        }

        /// <summary>
        /// The display mention popup.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool DisplayMentionPopup()
        {
            return this.PageContext.Mention > 0;
        }

        /// <summary>
        /// The display quoted popup.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool DisplayQuotedPopup()
        {
            return this.PageContext.Quoted > 0;
        }

        /// <summary>
        /// Displays the PM popup.
        /// </summary>
        /// <returns>
        /// The display pm popup.
        /// </returns>
        protected bool DisplayPmPopup()
        {
            return this.PageContext.UnreadPrivate > 0
                   && this.PageContext.LastUnreadPm > this.Get<IYafSession>().LastPm;
        }

        /// <summary>
        /// The last pending buddies.
        /// </summary>
        /// <returns>
        /// whether we should display the pending buddies notification or not
        /// </returns>
        protected bool DisplayPendingBuddies()
        {
            return this.PageContext.PendingBuddies > 0
                   && this.PageContext.LastPendingBuddies > this.Get<IYafSession>().LastPendingBuddies;
        }

        /// <summary>
        /// Handles the Load event of the ForumPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ForumPageLoad([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.GeneratePopUp();
        }

        /// <summary>
        /// Creates this pages title and fires a PageTitleSet event if one is set
        /// </summary>
        private void GeneratePopUp()
        {
            var notification = (DialogBox)this.PageContext.CurrentForumPage.Notification;

            // This happens when user logs in
            if (this.DisplayQuotedPopup() && !this.PageContext.ForumPageType.Equals(obj: ForumPages.posts))
            {
                notification.Show(
                    message: this.GetTextFormatted("UNREAD_QUOTED_MSG", this.PageContext.Quoted),
                    title: this.GetText(page: "COMMON", tag: "UNREAD_QUOTED_TITLE"),
                    okay: new DialogButton
                                  {
                                      Text = this.GetText(page: "COMMON", tag: "YES"),
                                      CssClass = "btn btn-success btn-sm",
                                      ForumPageLink = new ForumLink
                                                          {
                                                              ForumPage = ForumPages.posts,
                                                              ForumLinkFormat = "m={0}#post{0}",
                                                              ForumLinkArgs = new object[] { this.PageContext.LastQuoted }
                                                          }
                                  },
                    cancel: new DialogButton
                                      {
                                          Text = this.GetText(page: "COMMON", tag: "NO"),
                                          CssClass = "btn btn-danger btn-sm",
                                          ForumPageLink =
                                              new ForumLink { ForumPage = YafContext.Current.ForumPageType }
                                      });

                // Avoid Showing Both Popups
                return;
            }

            if (this.DisplayReceivedThanksPopup() && !this.PageContext.ForumPageType.Equals(obj: ForumPages.posts))
            {
                notification.Show(
                    message: this.GetTextFormatted("UNREAD_THANKS_MSG", this.PageContext.ReceivedThanks),
                    title: this.GetText(page: "COMMON", tag: "UNREAD_THANKS_TITLE"),
                    okay: new DialogButton
                                  {
                                      Text = this.GetText(page: "COMMON", tag: "YES"),
                                      CssClass = "btn btn-success btn-sm",
                                      ForumPageLink = new ForumLink
                                                          {
                                                              ForumPage = ForumPages.posts,
                                                              ForumLinkFormat = "m={0}#post{0}",
                                                              ForumLinkArgs = new object[] { this.PageContext.LastReceivedThanks }
                                                          }
                                  },
                    cancel: new DialogButton
                                      {
                                          Text = this.GetText(page: "COMMON", tag: "NO"),
                                          CssClass = "btn btn-danger btn-sm",
                                          ForumPageLink =
                                              new ForumLink { ForumPage = YafContext.Current.ForumPageType }
                                      });

                // Avoid Showing Both Popups
                return;
            }

            if (this.DisplayMentionPopup() && !this.PageContext.ForumPageType.Equals(obj: ForumPages.posts))
            {
                notification.Show(
                    message: this.GetTextFormatted("UNREAD_MENTION_MSG", this.PageContext.Mention),
                    title: this.GetText(page: "COMMON", tag: "UNREAD_MENTION_TITLE"),
                    okay: new DialogButton
                                  {
                                      Text = this.GetText(page: "COMMON", tag: "YES"),
                                      CssClass = "btn btn-success btn-sm",
                                      ForumPageLink = new ForumLink
                                                          {
                                                              ForumPage = ForumPages.posts,
                                                              ForumLinkFormat = "m={0}#post{0}",
                                                              ForumLinkArgs = new object[] { this.PageContext.LastMention }
                                                          }
                                  },
                    cancel: new DialogButton
                                      {
                                          Text = this.GetText(page: "COMMON", tag: "NO"),
                                          CssClass = "btn btn-danger btn-sm",
                                          ForumPageLink =
                                              new ForumLink { ForumPage = YafContext.Current.ForumPageType }
                                      });

                // Avoid Showing Both Popups
                return;
            }

            if (this.DisplayPmPopup() && (!this.PageContext.ForumPageType.Equals(obj: ForumPages.cp_pm)
                                          || !this.PageContext.ForumPageType.Equals(obj: ForumPages.cp_editbuddies)))
            {
                notification.Show(
                    message:
                    this.GetTextFormatted("UNREAD_MSG2", this.PageContext.UnreadPrivate),
                    title: this.GetText(page: "COMMON", tag: "UNREAD_MSG_TITLE"),
                    okay: new DialogButton
                                  {
                                      Text = this.GetText(page: "COMMON", tag: "YES"),
                                      CssClass = "btn btn-success btn-sm",
                                      ForumPageLink = new ForumLink { ForumPage = ForumPages.cp_pm }
                                  },
                    cancel: new DialogButton
                                      {
                                          Text = this.GetText(page: "COMMON", tag: "NO"),
                                          CssClass = "btn btn-danger btn-sm",
                                          ForumPageLink =
                                              new ForumLink { ForumPage = YafContext.Current.ForumPageType }
                                      });

                this.Get<IYafSession>().LastPm = this.PageContext.LastUnreadPm;

                // Avoid Showing Both Popups
                return;
            }

            if (!this.DisplayPendingBuddies() || this.PageContext.ForumPageType.Equals(obj: ForumPages.cp_editbuddies) || this.PageContext.ForumPageType.Equals(obj: ForumPages.cp_pm))
            {
                return;
            }

            notification.Show(
                message:
                this.GetTextFormatted("PENDINGBUDDIES2", this.PageContext.PendingBuddies),
                title: this.GetText(page: "BUDDY", tag: "PENDINGBUDDIES_TITLE"),
                okay: new DialogButton
                              {
                                  Text = this.GetText(page: "COMMON", tag: "YES"),
                                  CssClass = "btn btn-success btn-sm",
                                  ForumPageLink = new ForumLink { ForumPage = ForumPages.cp_editbuddies }
                              },
                cancel: new DialogButton
                                  {
                                      Text = this.GetText(page: "COMMON", tag: "NO"),
                                      CssClass = "btn btn-danger btn-sm",
                                      ForumPageLink =
                                          new ForumLink { ForumPage = YafContext.Current.ForumPageType }
                                  });

            this.Get<IYafSession>().LastPendingBuddies = this.PageContext.LastPendingBuddies;
        }
    }

    #endregion
}