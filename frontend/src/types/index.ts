export type Pessoa = { id: number; nome: string; idade: number }
export type TipoTransacao = 1 | 2
export type Transacao = { id: number; descricao: string; valor: number; tipo: TipoTransacao; pessoaId: number; pessoaNome: string }
export type TotalPessoa = { pessoaId: number; nome: string; totalReceitas: number; totalDespesas: number; saldo: number }
export type Totais = { pessoas: TotalPessoa[]; totalGeralReceitas: number; totalGeralDespesas: number; saldoLiquidoGeral: number }
