using Markdig.Renderers;
using Markdig.Syntax.Inlines;

namespace Markdig.Gist
{
    public interface IMarkdigGistWrapper
    {
        void Wrap(string gistEmbed, HtmlRenderer renderer, LinkInline linkInline);
    }
}
