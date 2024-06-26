﻿namespace TweetyProject.NET.Commons.postulates;

using Formula = Formula;

/// <summary>
/// Evaluates some approach (reasoner, measure, etc.) wrt. a series
/// of rationality postulates on a given series of knowledge bases.
/// 
/// @author Matthias Thimm
/// </summary>
/// @param <T> The type of formulas used in the evaluation. </param>
/// @param <U> The type of belief bases used in the evaluation. </param>
public class PostulateEvaluator<TT, TU, TS> 
    where TT : Formula
    where TU : BeliefSet<TT, TS>
    where TS : Signature
{

    /// <summary>
    /// The belief base sampler used to test the rationality postulates
    /// </summary>
    private readonly IBeliefSetIterator<TT, TU> _iterator;

    /// <summary>
    /// The approach being evaluated.
    /// </summary>
    private readonly PostulateEvaluatable<TT> _ev;

    /// <summary>
    /// the list of postulates the approach is evaluated against
    /// </summary>
    private IList<Postulate<TT>> _postulates = new List<Postulate<TT>>();

    /// <summary>
    /// Creates a new evaluator for the given evaluatable and
    /// belief base generator. </summary>
    /// <param name="iterator"> some belief set iterator </param>
    /// <param name="ev"> some evaluatable </param>
    /// <param name="postulates"> a set of postulates </param>
    public PostulateEvaluator(IBeliefSetIterator<TT, TU> iterator, PostulateEvaluatable<TT> ev, ICollection<Postulate<TT>> postulates)
    {
        _iterator = iterator;
        _ev = ev;
        ((List<Postulate<TT>>)_postulates).AddRange(postulates);
    }

    /// <summary>
    /// Creates a new evaluator for the given evaluatable and
    /// belief base generator. </summary>
    /// <param name="iterator"> some belief set iterator </param>
    /// <param name="ev"> some evaluatable </param>
    public PostulateEvaluator(IBeliefSetIterator<TT, TU> iterator, PostulateEvaluatable<TT> ev)
    {
        _iterator = iterator;
        _ev = ev;
    }

    /// <summary>
    /// Adds the given postulate </summary>
    /// <param name="p"> some postulate </param>
    public virtual void AddPostulate(Postulate<TT> p)
    {
        _postulates.Add(p);
    }

    /// <summary>
    /// Adds all postulates in the given collection. </summary>
    /// <param name="postulates"> some postulates </param>
    public virtual void AddAllPostulates<T1>(ICollection<T1> postulates) where T1 : Postulate<TT>
    {
        foreach (Postulate<TT> p in postulates)
        {
            AddPostulate(p);
        }
    }

    /// <summary>
    /// Removes the given postulate </summary>
    /// <param name="p"> some postulate </param>
    /// <returns> true if this contained the specified postulate. </returns>
    public virtual bool RemovePostulate(Postulate<TT> p)
    {
        return _postulates.Remove(p);
    }

    /// <summary>
    /// Removes all postulates in the given collection. </summary>
    /// <param name="postulates"> some postulates </param>
    public virtual void RemoveAllPostulates<T1>(ICollection<T1> postulates) where T1 : Postulate<TT>
    {
        foreach (Postulate<TT> p in postulates)
        {
            RemovePostulate(p);
        }
    }

    /// <summary>
    /// Evaluates all postulates of this evaluator on the given 
    /// approach on <code>num</code> belief bases generated by
    /// the sampler of this evaluator. </summary>
    /// <param name="num"> the number of belief bases to be applied. </param>
    /// <param name="stopWhenFailed"> if true the evaluation of one postulate
    /// 	will be stopped once a violation has been encountered. </param>
    /// <returns> a report on the evaluation </returns>
    public virtual PostulateEvaluationReport<TT> Evaluate(long num, bool stopWhenFailed)
    {
        PostulateEvaluationReport<TT> rep = new PostulateEvaluationReport<TT>(_ev,_postulates);

        ICollection<Postulate<TT>> failedPostulates = new HashSet<Postulate<TT>>();

        for (int i = 0; i < num; i++)
        {
            TU instance = _iterator.Next();

            foreach (Postulate<TT> postulate in _postulates)
            {
                if (stopWhenFailed && failedPostulates.Contains(postulate))
                {
                    continue;
                }
                if (!postulate.IsApplicable(instance))
                {
                    rep.AddNotApplicableInstance(postulate, instance);
                }
                else if (postulate.IsSatisfied(instance, _ev))
                {
                    rep.AddPositiveInstance(postulate, instance);
                }
                else
                {
                    rep.AddNegativeInstance(postulate, instance);
                    failedPostulates.Add(postulate);
                }
            }

            Console.WriteLine(rep);
        }
        return rep;
    }

    /// <summary>
    /// Evaluates all postulates of this evaluator on the given 
    /// approach on <code>num</code> belief bases generated by
    /// the sampler of this evaluator. The evaluation of any 
    /// one postulate will be stopped once a violation has been
    /// encountered. </summary>
    /// <param name="num"> the number of belief bases to be applied. </param>
    /// <returns> a report on the evaluation </returns>
    public virtual PostulateEvaluationReport<TT> Evaluate(long num)
    {
        return Evaluate(num, true);
    }
}