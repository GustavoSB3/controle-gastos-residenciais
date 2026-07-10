export function Mensagem({ texto, tipo = 'erro' }: { texto: string; tipo?: 'erro' | 'sucesso' }) {
  return <p className={`mensagem ${tipo}`}>{texto}</p>
}
