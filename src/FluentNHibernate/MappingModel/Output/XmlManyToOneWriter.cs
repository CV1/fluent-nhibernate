using System.Xml;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlManyToOneWriter : NullMappingModelVisitor, IXmlWriter<ManyToOneMapping>
    {
        private readonly IXmlWriterServiceLocator serviceLocator;
        private XmlDocument document;

        public XmlManyToOneWriter(IXmlWriterServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public XmlDocument Write(ManyToOneMapping mappingModel)
        {
            document = null;
            mappingModel.AcceptVisitor(this);
            return document;
        }

        public override void ProcessManyToOne(ManyToOneMapping mapping)
        {
            document = new XmlDocument();

            var element = document.AddElement("many-to-one");

            if (mapping.IsSpecified(x => x.Access))
                element.WithAtt("access", mapping.Access);

            if (mapping.IsSpecified(x => x.Cascade))
                element.WithAtt("cascade", mapping.Cascade);

            if (mapping.IsSpecified(x => x.Class))
                element.WithAtt("class", mapping.Class);

            if (mapping.IsSpecified(x => x.Fetch))
                element.WithAtt("fetch", mapping.Fetch);

            if (mapping.IsSpecified(x => x.ForeignKey))
                element.WithAtt("foreign-key", mapping.ForeignKey);

            if (mapping.IsSpecified(x => x.Insert))
                element.WithAtt("insert", mapping.Insert);

            if (mapping.IsSpecified(x => x.Lazy))
                element.WithAtt("lazy", mapping.Lazy);

            if (mapping.IsSpecified(x => x.Name))
                element.WithAtt("name", mapping.Name);

            if (mapping.IsSpecified(x => x.NotFound))
                element.WithAtt("not-found", mapping.NotFound);

            if (mapping.IsSpecified(x => x.OuterJoin))
                element.WithAtt("outer-join", mapping.OuterJoin);

            if (mapping.IsSpecified(x => x.PropertyRef))
                element.WithAtt("property-ref", mapping.PropertyRef);

            if (mapping.IsSpecified(x => x.Update))
                element.WithAtt("update", mapping.Update);
        }

        public override void Visit(ColumnMapping columnMapping)
        {
            var writer = serviceLocator.GetWriter<ColumnMapping>();
            var xml = writer.Write(columnMapping);

            document.ImportAndAppendChild(xml);
        }
    }
}