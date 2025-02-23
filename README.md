# SingleRangeBar Trading Strategy

The **SingleRangeBar** project implements an algorithmic trading strategy for use with a trading platform such as Quantower or other compatible trading platforms. This strategy is based on analyzing **range bars** to make buy or sell decisions. It automates trading operations such as placing, removing, and managing positions, leveraging historical and real-time data.

---

## 📖 Features

- **Historical Data Analysis**: Fetches and processes range bar historical data for strategy computation.
- **Real-Time Tracking**: Monitors live price updates and acts upon them dynamically.
- **Event-Driven Execution**: Handles key platform events including position changes and trades.
- **Flexible Configuration**: Customizable parameters for range bar size, number of contracts, etc.
- **Automated Trading**: Automatically places or removes trades based on range bar evaluations.
- **Supports Both Long and Short Trades** by evaluating the bar's price action.

---

## 🔧 Requirements

- **.NET 8.0**: The project is built using .NET 8.0 and requires the runtime.
- Dependencies:
  - `IPlatform`: Interface for platform-specific operations (e.g., handling positions and trades).
  - `IBroker`: Interface for broker-related operations (e.g., placing market orders).
  - `IQuantowerAsset`: Interface for accessing symbol and historical data.
  - `ILogger`: Interface for logging system activity.
  - `IConfiguration`: Interface for custom strategy configurations.
- Compatible Trading Platform APIs, such as Quantower.

---

## 🚀 Usage

This strategy leverages **range bars** to detect and act upon market trends. Here's a breakdown of how it operates:

1. **Initialization**:
   - Connects to the platform, initializes data sources, and subscribes to platform events such as price updates, position changes, and trade updates.

2. **Historical Data Loading**:
   - Retrieves historical range bar data to analyze the market over a specified time period:
     ```c#
     rangeBarData = asset.Symbol.GetHistory(new HistoryRequestParameters
     {
         Symbol = asset.Symbol,
         FromTime = Core.Instance.TimeUtils.DateTimeUtcNow.AddDays(-10),
         Aggregation = new HistoryAggregationRangeBars(configuration.RangeBarSize, asset.Symbol.HistoryType)
     });
     ```

3. **Real-Time Monitoring**:
   - Listens for the following events:
     - **Price Updates**: Monitors market prices in real time.
     - **Range Bar Updates**: Uses new bar data to decide whether to take a long or short position.
     - **Position and Trade Events**: Tracks open/close positions and trade executions.

4. **Trading Logic**:
   - Based on the most recent range bar, the strategy determines the price action:
     - **Upward Movement**: `Side.Buy`
     - **Downward Movement**: `Side.Sell`
   - Executes a market order via the broker API:
     ```c#
     broker.Market(
         PreviousRangeBar[PriceType.Open] < PreviousRangeBar[PriceType.Close] ? Side.Buy : Side.Sell,
         configuration.Contracts
     );
     ```

5. **Shutdown**:
   - Cleans up resources, closes all positions, and unsubscribes from platform events.

---

## 🛠️ Methods Reference

### **Algo.Start()**

Initializes and starts the trading algorithm:
- Subscribes to platform events (`PriceUpdated`, `PositionAdded`, etc.).
- Loads historical range bar data for analysis.

### **Algo.Stop()**

Stops the algorithm:
- Unsubscribes from platform events.
- Closes all active positions using the broker.

### **OnNewHistoryItem(sender, e)**
Fired when new range bar data becomes available:
- Evaluates if the latest bar signifies an upward or downward market move.
- Places a market order accordingly.

---

## ⚙️ Configuration

The strategy relies on an `IConfiguration` implementation for dynamic settings, including:

- **RangeBarSize**: Determines the range bar aggregation period.
- **Contracts**: The number of contracts to trade per order.

Example:
```json
{
  "RangeBarSize": 5,
  "Contracts": 2
}
```

---

## 💻 Development and Customization

### Prerequisites
- Install the **.NET 8.0 SDK** on your system.
- Set up an IDE like **JetBrains Rider** or **Visual Studio**.

### Project Code Structure
- **Strategy Logic**: `SingleRangeBar.Strategy.Algo`
- **Models**: Contains the necessary data structures for working with platforms, brokers, and the trading marketplace.
- **Logging and Configuration**:
  - `ILogger` is used to log critical events during runtime.
  - `IConfiguration` provides runtime configuration details for the strategy.

---

## ✨ How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/The-Coding-Trader/single-range-bar-strategy.git
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Build and run the application:
   ```bash
   dotnet run
   ```

---

## 🧑‍💻 Contribution

Contributions are welcome! Please follow these steps:

1. Fork the project.
2. Create a feature or bugfix branch:
   ```bash
   git checkout -b feature-name
   ```
3. Commit changes and open a pull request.

---

## 📜 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

---

## 💬 Questions?

Feel free to reach out if you have any questions or suggestions. Open an issue or contact the repository maintainers directly.
