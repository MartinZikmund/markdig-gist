// Copyright 2020 (c) Martin Zikmund. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Markdig.Gist
{
    public static class MarkdigGistExtensions
    {
        public static MarkdownPipelineBuilder UseGistExtension(this MarkdownPipelineBuilder pipeline)
        {
            pipeline.Extensions.Add(new MarkdigGistExtension());
            return pipeline;
        }

        public static MarkdownPipelineBuilder UseGistExtension(this MarkdownPipelineBuilder pipeline, IMarkdigGistWrapper wrapper)
        {
            pipeline.Extensions.Add(new MarkdigGistExtension(wrapper));
            return pipeline;
        }
    }
}
