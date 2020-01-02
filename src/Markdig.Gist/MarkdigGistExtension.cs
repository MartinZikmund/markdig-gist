// Copyright 2020 (c) Martin Zikmund. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using Markdig.Renderers;
using Markdig.Renderers.Html.Inlines;
using Markdig.Syntax.Inlines;
using System;
using System.Globalization;

namespace Markdig.Gist
{
    /// <summary>
    /// Extension for easy embedding of GitHub Gists
    /// </summary>
    /// <seealso cref="IMarkdownExtension" />
    public class MarkdigGistExtension : IMarkdownExtension
    {
        private const string GithubGistHost = "gist.github.com";
        private const string GistScriptUriEnding = ".js";
        private const string GistEmbedFormatString = "<script src=\"{0}\"></script>";

        private readonly IMarkdigGistWrapper _wrapper;

        public MarkdigGistExtension(IMarkdigGistWrapper wrapper = null) => _wrapper = wrapper;

        public void Setup(MarkdownPipelineBuilder pipeline)
        {
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            var htmlRenderer = renderer as HtmlRenderer;
            if (htmlRenderer != null)
            {
                var inlineRenderer = htmlRenderer.ObjectRenderers.FindExact<LinkInlineRenderer>();
                if (inlineRenderer != null)
                {
                    inlineRenderer.TryWriters.Remove(TryRenderGist);
                    inlineRenderer.TryWriters.Add(TryRenderGist);
                }
            }
        }

        private bool TryRenderGist(HtmlRenderer renderer, LinkInline linkInline)
        {
            if (linkInline.Url == null) return false;

            if (!Uri.TryCreate(linkInline.Url, UriKind.RelativeOrAbsolute, out var uri) || !uri.IsAbsoluteUri) return false;

            if (uri.Host != GithubGistHost) return false;

            RenderGist(uri, renderer, linkInline);

            return true;
        }

        private void RenderGist(Uri uri, HtmlRenderer renderer, LinkInline linkInline)
        {
            var uriString = uri.AbsoluteUri.ToString().Trim('/');

            if (!uriString.EndsWith(GistScriptUriEnding, StringComparison.InvariantCultureIgnoreCase))
            {
                uriString += GistScriptUriEnding;
            }

            var scriptHtml = string.Format(CultureInfo.InvariantCulture, GistEmbedFormatString, uriString);

            if (_wrapper != null)
            {
                _wrapper.Wrap(scriptHtml, renderer, linkInline);
            }
            else
            {
                renderer.Write(scriptHtml);
            }            
        }        
    }
}