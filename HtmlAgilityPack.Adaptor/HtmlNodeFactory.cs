﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AP = HtmlAgilityPack;

namespace Ivony.Html.HtmlAgilityPackAdaptor
{
  public class HtmlNodeFactory : IHtmlNodeFactory
  {
    private AP.HtmlDocument _document;

    internal HtmlNodeFactory( AP.HtmlDocument document )
    {
      _document = document;
    }



    #region IHtmlNodeFactory 成员

    public IFreeElement CreateElement( string name )
    {
      return new FreeElementAdaptor( this, _document.CreateElement( name ) );
    }

    public IFreeTextNode CreateTextNode( string htmlText )
    {
      return new FreeTextNodeAdaptor( this, _document.CreateTextNode( htmlText ) );
    }

    public IFreeComment CreateComment( string comment )
    {
      return new FreeCommentAdaptor( this, _document.CreateComment( comment ) );
    }



    public HtmlFragment ParseFragment( string html )
    {
      var document = new AP.HtmlDocument();

      document.LoadHtml( html );

      var fragment = new HtmlFragment( this );
      fragment.AddCopies( document.AsDocument().Nodes() );

      return fragment;
    }


    public IHtmlDocument Document
    {
      get { return _document.AsDocument(); }
    }


    #endregion


    private class FreeElementAdaptor : HtmlElementAdapter, IFreeElement
    {

      private HtmlNodeFactory _factory;

      public FreeElementAdaptor( HtmlNodeFactory factory, AP.HtmlNode node )
        : base( node )
      {
        if ( node.ParentNode != null )
          throw new InvalidOperationException();

        _factory = factory;
      }


      public IHtmlNode Into( IHtmlContainer container, int index )
      {
        if ( container == null )
          throw new ArgumentNullException( "container" );

        var containerAdapter = container as HtmlContainerAdapter;
        if ( containerAdapter == null )
          throw new InvalidOperationException();


        containerAdapter.Node.ChildNodes.Insert( index, Node );

        return Node.AsNode();
      }


      public IHtmlNodeFactory Factory
      {
        get { return _factory; }
      }


      IHtmlContainer IHtmlNode.Container
      {
        get { return null; }
      }

      IHtmlDocument IHtmlDomObject.Document
      {
        get { return _factory._document.AsDocument(); }
      }
    }


    private class FreeTextNodeAdaptor : HtmlTextNodeAdapter, IFreeTextNode
    {
      private HtmlNodeFactory _factory;

      public FreeTextNodeAdaptor( HtmlNodeFactory factory, AP.HtmlTextNode node )
        : base( node )
      {
        if ( node.ParentNode != null )
          throw new InvalidOperationException();

        _factory = factory;
      }



      public IHtmlNode Into( IHtmlContainer container, int index )
      {
        if ( container == null )
          throw new ArgumentNullException( "container" );

        var containerAdapter = container as HtmlContainerAdapter;
        if ( containerAdapter == null )
          throw new InvalidOperationException();


        containerAdapter.Node.ChildNodes.Insert( index, Node );

        return Node.AsNode();
      }


      public IHtmlNodeFactory Factory
      {
        get { return _factory; }
      }


      IHtmlContainer IHtmlNode.Container
      {
        get { return null; }
      }

      IHtmlDocument IHtmlDomObject.Document
      {
        get { return _factory._document.AsDocument(); }
      }
    }


    private class FreeCommentAdaptor : HtmlCommentNodeAdapter, IFreeComment
    {
      private HtmlNodeFactory _factory;

      public FreeCommentAdaptor( HtmlNodeFactory factory, AP.HtmlCommentNode node )
        : base( node )
      {
        if ( node.ParentNode != null )
          throw new InvalidOperationException();

        _factory = factory;
      }



      public IHtmlNode Into( IHtmlContainer container, int index )
      {
        if ( container == null )
          throw new ArgumentNullException( "container" );

        var containerAdapter = container as HtmlContainerAdapter;
        if ( containerAdapter == null )
          throw new InvalidOperationException();


        containerAdapter.Node.ChildNodes.Insert( index, Node );

        return Node.AsNode();
      }


      public IHtmlNodeFactory Factory
      {
        get { return _factory; }
      }


      IHtmlContainer IHtmlNode.Container
      {
        get { return null; }
      }

      IHtmlDocument IHtmlDomObject.Document
      {
        get { return _factory._document.AsDocument(); }
      }
    }

  }
}
