## All SOLID Violations (Exercise)

This project intentionally violates *all* SOLID principles in a single scenario.

### Your goal
Refactor the code so it adheres to SOLID without changing the external behaviour of `MegaOrderService.PlaceOrder(...)`.

### Where to start
- `MegaOrderService` in `Exercise.cs`
- Persistence types in `OrderStore.cs`, `ReadOnlyOrderStore.cs`, `FileOrderStore.cs`

### Shared models
This project references `SharedModels` and uses the shared order models/enums from `AllSolid.Shared.Models`.
