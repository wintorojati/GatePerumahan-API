# 🏠 Housing Gate Access System (RFID + REST API)

This project implements a **housing gate access system** using **RFID cards**, an **ESP32/Arduino**, and a **REST API backend**.

---

## 📌 Overview
Residents use an RFID card at the gate.  
The card UID is read by an **MFRC522 RFID reader** connected to an ESP32 (or Arduino Uno + W5100).  
The UID is sent via **HTTP REST API** to a local server, which decides whether to grant access.

---

## 🔧 System Architecture

- **ESP32 + MFRC522** reads RFID card and communicates with server.
- **REST API (e.g., .NET Core 9)** checks UID validity.
- **Relay Module** controls the gate motor/door lock.
- **Database** stores residents and access logs.

---

## ⚙️ Hardware
- **Option A: Arduino Uno + W5100 Ethernet Shield + MFRC522**
  - Pros: Stable wired LAN
  - Cons: Limited memory, no HTTPS
- **Option B: ESP32 (Wi-Fi) + MFRC522**
  - Pros: Fast, supports HTTPS, more memory
  - Cons: Wi-Fi less stable than LAN
- **Option C: ESP32 + W5500/LAN8720/WT32-ETH01**
  - Pros: Best of both (LAN + ESP32 power)

### RFID Reader (MFRC522 → ESP32)
| MFRC522 Pin | ESP32 Pin Example |
|-------------|-------------------|
| SDA (SS)    | GPIO 5            |
| SCK         | GPIO 18           |
| MOSI        | GPIO 23           |
| MISO        | GPIO 19           |
| RST         | GPIO 22           |
| VCC         | 3.3V              |
| GND         | GND               |

---

## 🖥️ Backend REST API
Example endpoints:
- `GET /api/access/check?uid=xxxx&gate=1`
  ```json
  { "access": 1 }
  ```
or
```json
{ "access": 0 }
```

Database schema:
- Residents (Id, Name, CardUID, Status)
- AccessLogs (Id, CardUID, Gate, Timestamp, Result)

## Example ESP32 Wi-Fi Code
```cpp
#include <WiFi.h>
#include <HTTPClient.h>
#include <SPI.h>
#include <MFRC522.h>

#define SS_PIN 5
#define RST_PIN 22

MFRC522 mfrc522(SS_PIN, RST_PIN);

// Wi-Fi credentials
const char* ssid = "YourSSID";
const char* password = "YourPassword";
String serverUrl = "http://192.168.1.100/api/access/check?uid=";

int relayPin = 4;

void setup() {
  Serial.begin(115200);
  pinMode(relayPin, OUTPUT);
  digitalWrite(relayPin, LOW);

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nWiFi Connected");

  SPI.begin(18, 19, 23); // SCK, MISO, MOSI
  mfrc522.PCD_Init();
  Serial.println("ESP32 RFID Gate Ready");
}

void loop() {
  if (!mfrc522.PICC_IsNewCardPresent() || !mfrc522.PICC_ReadCardSerial())
    return;

  // Build UID string
  String uid = "";
  for (byte i = 0; i < mfrc522.uid.size; i++) {
    uid += String(mfrc522.uid.uidByte[i], HEX);
  }
  uid.toUpperCase();
  Serial.println("Card UID: " + uid);

  // Send request
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    String url = serverUrl + uid + "&gate=1";
    http.begin(url);

    int httpCode = http.GET();
    if (httpCode == 200) {
      String payload = http.getString();
      Serial.println("Response: " + payload);

      if (payload.indexOf("1") > 0) {
        Serial.println("Access Granted");
        digitalWrite(relayPin, HIGH);
        delay(3000);
        digitalWrite(relayPin, LOW);
      } else {
        Serial.println("Access Denied");
      }
    } else {
      Serial.printf("HTTP Error: %d\n", httpCode);
    }
    http.end();
  }
  mfrc522.PICC_HaltA();
}
```
### 📊 Performance Considerations

#### Wi-Fi (ESP32):
- Latency: 2–20 ms (fast enough for gates).
- Suitable if access point is nearby.

#### LAN (Arduino + W5100 or ESP32 + W5500):
- More stable, ideal for main gates.
- No risk of Wi-Fi signal loss.

### 🚀 Enhancements
- Store recent valid UIDs in EEPROM for offline fallback.
- Add buzzer/LED feedback on denial.
- Use HTTPS if backend requires security (ESP32 only).
- Centralize logs in database for audit.

### 📌 Conclusion
- For simplicity & reliability: use ESP32 with LAN (WT32-ETH01 or W5500).
- For easy installation: use ESP32 with Wi-Fi if network is stable.
- Arduino Uno + W5100 works, but ESP32 is faster, more future-proof, and supports HTTPS.