//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from .\Kay.g4 by ANTLR 4.12.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="KayParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
public interface IKayListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.cycle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCycle([NotNull] KayParser.CycleContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.cycle"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCycle([NotNull] KayParser.CycleContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.simpleDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSimpleDefinition([NotNull] KayParser.SimpleDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.simpleDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSimpleDefinition([NotNull] KayParser.SimpleDefinitionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerm([NotNull] KayParser.TermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerm([NotNull] KayParser.TermContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFactor([NotNull] KayParser.FactorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFactor([NotNull] KayParser.FactorContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.setLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSetLiteral([NotNull] KayParser.SetLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.setLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSetLiteral([NotNull] KayParser.SetLiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.quotationLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterQuotationLiteral([NotNull] KayParser.QuotationLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.quotationLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitQuotationLiteral([NotNull] KayParser.QuotationLiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.atomicSymbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtomicSymbol([NotNull] KayParser.AtomicSymbolContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.atomicSymbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtomicSymbol([NotNull] KayParser.AtomicSymbolContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.booleanConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBooleanConstant([NotNull] KayParser.BooleanConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.booleanConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBooleanConstant([NotNull] KayParser.BooleanConstantContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.integerConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIntegerConstant([NotNull] KayParser.IntegerConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.integerConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIntegerConstant([NotNull] KayParser.IntegerConstantContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.floatConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFloatConstant([NotNull] KayParser.FloatConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.floatConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFloatConstant([NotNull] KayParser.FloatConstantContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.characterConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCharacterConstant([NotNull] KayParser.CharacterConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.characterConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCharacterConstant([NotNull] KayParser.CharacterConstantContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="KayParser.stringConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStringConstant([NotNull] KayParser.StringConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="KayParser.stringConstant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStringConstant([NotNull] KayParser.StringConstantContext context);
}
