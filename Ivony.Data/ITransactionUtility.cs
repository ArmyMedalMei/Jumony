using System;
using System.Collections.Generic;
using System.Text;

namespace Ivony.Data
{
  public interface ITransactionUtility : IDisposable
  {

    void Begin();

    /// <summary>
    /// �ύ����
    /// </summary>
    void Commit();

    /// <summary>
    /// �ع�����
    /// </summary>
    void Rollback();

    /// <summary>
    /// ��ȡ����ִ��SQL����DbUtilityʵ����
    /// </summary>
    DbUtility DbUtility
    {
      get;
    }
  }

  public interface ITransactionUtility<T> : ITransactionUtility where T : DbUtility
  {
    T DbUtility
    {
      get;
    }
  }


}
